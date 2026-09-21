using FluentValidation;
using PsychologistSystem.Application.Contracts.Email;
using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.DTOs.Psychologist;
using PsychologistSystem.Application.Exceptions;
using PsychologistSystem.Application.Interfaces;
using PsychologistSystem.Application.Interfaces.Repositories;
using PsychologistSystem.Application.Interfaces.Repositories.Psychologists;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Application.Interfaces.Services.Email;
using PsychologistSystem.Application.Interfaces.Services.Psychologists;
using PsychologistSystem.Application.Validators.Psychologist;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Domain.Enums;

namespace PsychologistSystem.Application.Services.Psychologists
{
    public class PsychologistApplicationService : IPsychologistApplicationService
    {
        private readonly IPsychologistRepository _psychologistRepository;
        private readonly IPsychologistApplicationRepository _applicationRepository;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IValidator<RejectApplicationRequest> _rejectValidator;

        public PsychologistApplicationService(
            IPsychologistRepository psychologistRepository, 
            IPsychologistApplicationRepository applicationRepository,
            IIdentityService identityService,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IRefreshTokenRepository refreshTokenRepository,
            IValidator<RejectApplicationRequest> rejectValidator
        )
        {
            _psychologistRepository = psychologistRepository;
            _applicationRepository = applicationRepository;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _refreshTokenRepository = refreshTokenRepository;
            _rejectValidator = rejectValidator;
        }
        public async Task ApproveApplicationAsync(Guid applicationId, Guid adminUserId, CancellationToken ct)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId, ct);

            if (application is null || application.Status != PsychologistApplicationStatus.Pending)
            {
                throw new NotFoundException("Application not found or already reviewed.");
            }

            var existingPsychologist = await _psychologistRepository.GetByUserIdAsync(application.UserId, ct);

            if (existingPsychologist is not null)
            {
                throw new InvalidOperationException("This user is already an approved psychologist.");
            }

            application.Status = PsychologistApplicationStatus.Approved;
            application.ReviewedAt = DateTimeOffset.UtcNow;
            application.ReviewedByUserId = adminUserId;

            // create the real Psychologist record
            _psychologistRepository.Add(new Psychologist
            {
                Id = Guid.NewGuid(),
                UserId = application.UserId,
                Description = application.Description
            });

            await _identityService.AddToRoleAsync(application.UserId, nameof(Role.Psychologist));

            // force fresh login so the new role takes effect immediately, not on next menual refresh
            await _refreshTokenRepository.RevokeAllForUserAsync(application.UserId);

            await _unitOfWork.SaveChangesAsync(ct);

            // notify user
            var email = await _identityService.GetEmailByIdAsync(application.UserId);

            if (!string.IsNullOrEmpty(email))
            {
                await _emailService.SendEmailAsync(new EmailMessage(
                    To: email,
                    Subject: "Your psychologist application was approved",
                    HtmlBody: "<p>Congratulations — your application has been approved. You can now set up your schedule.</p>"
                ));
            }
        }

        public async Task<IReadOnlyList<PsychologistApplication>> GetPendingApplicationsAsync(CancellationToken ct)
        {
            return await _applicationRepository.GetPendingAsync(ct);
        }

        public async Task RejectApplicationAsync(Guid applicationId, Guid adminUserId, RejectApplicationRequest request, CancellationToken ct)
        {
            await _rejectValidator.ValidateAndThrowAsync(request, ct);
            var application = await _applicationRepository.GetByIdAsync(applicationId, ct);

            if (application is null || application.Status != PsychologistApplicationStatus.Pending)
            {
                throw new InvalidOperationException("Application not found or already reviewed.");
            }

            application.Status = PsychologistApplicationStatus.Rejected;
            application.ReviewedAt = DateTimeOffset.UtcNow;
            application.ReviewedByUserId = adminUserId;

            await _unitOfWork.SaveChangesAsync(ct);

            // notify user
            var email = await _identityService.GetEmailByIdAsync(application.UserId);

            if (!string.IsNullOrEmpty(email))
            {
                await _emailService.SendEmailAsync(new EmailMessage(
                    To: email,
                    Subject: "Your psychologist application was rejected",
                    HtmlBody: "<p>We are sorry to inform you that your application has been rejected. For more details feel free to contact us on our Email</p>"
                ));
            }
        }

        public async Task SubmitApplicationAsync(Guid userId, SubmitApplicationRequest request, CancellationToken ct)
        {
            var existing = await _applicationRepository.GetPendingByUserIdAsync(userId, ct);

            if (existing is not null)
            {
                throw new InvalidOperationException("You already have a pending application.");
            }

            _applicationRepository.Add(new PsychologistApplication
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Status = PsychologistApplicationStatus.Pending,
                SubmittedAt = DateTimeOffset.UtcNow
            });

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
