using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService, IHttpContextService httpContextService)
        {
            _feedbackService = feedbackService;
            _httpContextService = httpContextService;
        }

        /// <summary>
        /// Add feedback for a learning module.
        /// </summary>
        [Authorize(Roles = CustomRoles.Learner)]
        [HttpPost("Learner/AddFeedback")]
        public async Task<IActionResult> AddFeedback([FromBody] LearnerFeedbackDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided.");
                }

                var user = await _httpContextService.GetAppUserAndThrow();

                var learningModule = await _feedbackService.GetLearningModuleById(dto.LearningModuleId);
                if (learningModule == null)
                {
                    return BadRequest("Invalid learning module ID.");
                }

                var isEnrolled = learningModule.EnrolledLearners.Any(learner => learner.Id == user.Id);
                if (!isEnrolled)
                {
                    return BadRequest("You are not enrolled in this learning module. Please enroll before giving feedback.");
                }

                var feedback = await _feedbackService.AddFeedback(dto, user);
                return Ok(feedback);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
