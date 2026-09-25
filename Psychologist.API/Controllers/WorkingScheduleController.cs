using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologistSystem.Application.DTOs.Schedule;
using PsychologistSystem.Application.Interfaces.Services.Schedule;
using PsychologistSystem.Domain.Enums;

namespace PsychologistSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(Role.Psychologist))]
    public class WorkingScheduleController : ControllerBase
    {
        private readonly IWorkingScheduleService _workingScheduleService;

        public WorkingScheduleController(IWorkingScheduleService workingScheduleService)
        {
            _workingScheduleService = workingScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMine(CancellationToken ct = default)
        {
            var res = await _workingScheduleService.GetMineAsync(ct);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateWorkingScheduleRequest req, CancellationToken ct = default)
        {
            var id =  await _workingScheduleService.CreateAsync(req, ct);

            return StatusCode(StatusCodes.Status201Created, new { id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(Guid id, CancellationToken ct = default)
        {
            await _workingScheduleService.DeleteAsync(id, ct);

            return NoContent();
        }
    }
}
