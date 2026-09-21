using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.DTOs.Psychologist;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Application.Interfaces.Services.Psychologists;
using PsychologistSystem.Application.Services.Auth;
using PsychologistSystem.Domain.Enums;
using System.Security.Claims;

namespace PsychologistSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PsychologistApplicationController : ControllerBase
    {
        private readonly IPsychologistApplicationService _applicationService;
        // using current user with [Authorize] attribute guarantees that user id is not null
        private readonly ICurrentUserService _currentUserService;

        public PsychologistApplicationController(IPsychologistApplicationService applicationService, ICurrentUserService currentUserService)
        {
            _applicationService = applicationService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit(SubmitApplicationRequest request, CancellationToken ct = default)
        {
            var userId = _currentUserService.UserId!.Value;

            await _applicationService.SubmitApplicationAsync(userId, request, ct);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("pending")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> GetPending(CancellationToken ct = default)
        {
            var applications = await _applicationService.GetPendingApplicationsAsync(ct);
            return Ok(applications);
        }

        [HttpPost("{applicationId:guid}/approve")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Approve(Guid applicationId, CancellationToken ct = default)
        {
            var adminUserId = _currentUserService.UserId!.Value;
            await _applicationService.ApproveApplicationAsync(applicationId, adminUserId, ct);

            return NoContent();
        }

        [HttpPost("{applicationId:guid}/reject")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Reject(Guid applicationId, RejectApplicationRequest request, CancellationToken ct = default)
        {
            var adminUserId = _currentUserService.UserId!.Value;
            await _applicationService.RejectApplicationAsync(applicationId, adminUserId, request.Reason, ct);

            return NoContent();
        }
    }
}
