using FluentValidation;
using PsychologistSystem.Application.DTOs.Schedule;
using PsychologistSystem.Application.Exceptions;
using PsychologistSystem.Application.Interfaces;
using PsychologistSystem.Application.Interfaces.Repositories.Psychologists;
using PsychologistSystem.Application.Interfaces.Repositories.Schedule;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Application.Interfaces.Services.Schedule;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Services.Schedule
{
    public sealed class WorkingScheduleService : IWorkingScheduleService
    {
        private readonly IWorkingScheduleRepository _workingScheduleRepository;
        private readonly IPsychologistRepository _psychologistRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateWorkingScheduleRequest> _createValidator;

        public WorkingScheduleService(
            IWorkingScheduleRepository workingScheduleRepository,
            IPsychologistRepository psychologistRepository,
            ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork,
            IValidator<CreateWorkingScheduleRequest> createValidator)
        {
            _workingScheduleRepository = workingScheduleRepository;
            _psychologistRepository = psychologistRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
        }
        public async Task<Guid> CreateAsync(CreateWorkingScheduleRequest request, CancellationToken ct)
        {
            await _createValidator.ValidateAndThrowAsync(request, ct);

            // throws if not
            var psychologist = await GetCurrentPsychologistAsync(ct);

            var existingBlocks = await _workingScheduleRepository
                .GetByPsychologistAndDayAsync(psychologist.Id, request.DayOfWeek, ct);

            // checks if incoming request overlaps with existing one
            var overlaps = existingBlocks.Any(b => request.StartTime < b.EndTime && request.EndTime > b.StartTime);

            if (overlaps)
            {
                throw new ConflictException("This time block overlaps an existing block on the same day.");
            }

            // TODO: add mapping in future!!!
            var scheduleRecord = new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                PsychologistId = psychologist.Id
            };
            _workingScheduleRepository.Add(scheduleRecord);

            await _unitOfWork.SaveChangesAsync(ct);

            return scheduleRecord.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            // throws if not
            var psychologist = await GetCurrentPsychologistAsync(ct);

            var schedule = await _workingScheduleRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException("Schedule was not found");

            // schedule ownership check
            if (schedule.PsychologistId != psychologist.Id)
            {
                throw new ForbiddenException("You can only delete your own schedule");
            }

            _workingScheduleRepository.Remove(schedule);
            
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<List<WorkingScheduleResponse>> GetMineAsync(CancellationToken ct)
        {
            var psychologist = await GetCurrentPsychologistAsync(ct);

            var schedules = await _workingScheduleRepository.GetByPsychologistIdAsync(psychologist.Id, ct);

            // TODO: select used same way as mapping, use mapping package in future!!!
            return schedules
                    .Select(s => new WorkingScheduleResponse(s.Id, s.DayOfWeek, s.StartTime, s.EndTime))
                    .ToList();
        }

        private async Task<Psychologist> GetCurrentPsychologistAsync(CancellationToken ct = default)
        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedException("Not authenticated.");

            var psychologist = await _psychologistRepository.GetByUserIdAsync(userId, ct)
                ?? throw new ForbiddenException("Only approved psychologists can manage working schedules.");

            return psychologist;
        }
    }
}
