using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
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
        private readonly ILearningModuleService _learningModuleService;

        public FeedbackController(IFeedbackService feedbackService, IHttpContextService httpContextService, ILearningModuleService learningModuleService)
        {
            _feedbackService = feedbackService;
            _httpContextService = httpContextService;
            _learningModuleService = learningModuleService;
        }

        [Authorize(Roles = CustomRoles.Learner)]
        [HttpPost("Learner/AddFeedback")]
        public async Task<IActionResult> AddFeedback([FromBody] LearnerFeedbackDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            var user = await _httpContextService.GetAppUserAndThrow();

            var learningModule = await _learningModuleService.GetLearningModuleById(dto.LearningModuleId);
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

        [Authorize(Roles = CustomRoles.GeneralUser)]
        [HttpPost("LikeFeedback/{feedbackId}")]
        public async Task<IActionResult> LikeFeedback(int feedbackId)
        {
            try
            {
                var user = await _httpContextService.GetAppUserAndThrow();
                var feedback = await _feedbackService.LikeFeedback(feedbackId, user);
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

        [Authorize(Roles = CustomRoles.GeneralUser)]
        [HttpPost("DislikeFeedback/{feedbackId}")]
        public async Task<IActionResult> DislikeFeedback(int feedbackId)
        {
            try
            {
                var user = await _httpContextService.GetAppUserAndThrow();
                var feedback = await _feedbackService.DisLikeFeedback(feedbackId, user);
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

        [Authorize(Roles = CustomRoles.GeneralUser)]
        [HttpGet("GetFeedbacksByLearningModuleId/{moduleId}")]
        public async Task<IActionResult> GetFeedbacksByLearningModuleId(int moduleId)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbacksByLearningModuleId(moduleId);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = CustomRoles.Admin)]
        [HttpGet("GetAverageRatingByStar/{moduleId}")]
        public async Task<IActionResult> GetAverageRatingByStar(int moduleId)
        {
            try
            {
                var averageRating = await _feedbackService.GetAverageRatingByStar(moduleId);
                return Ok(averageRating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = CustomRoles.Tutor)]
        [HttpPost("TutorReplyFeedback")]
        public async Task<IActionResult> TutorReplyFeedback([FromBody] TutorReplyFeedbackDto dto)
        {
            var user = await _httpContextService.GetAppUserAndThrow();
            try
            {
                var tutorReplies = await _feedbackService.ReplyToFeedback(dto, user);
                return Ok(tutorReplies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = CustomRoles.Tutor)]
        [HttpGet("GetReplyByFeedbackId/{feedbackId}")]
        public async Task<IActionResult> GetReplyByFeedbackId(int feedbackId)
        {
            try
            {
                var replies = await _feedbackService.GetReplyByFeedbackId(feedbackId);
                return Ok(replies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [Authorize(Roles = CustomRoles.GeneralUser)]
        [HttpGet("HasFeedback")]
        public async Task<bool> HasFeedback( int learningModuleId)
        {
            var learner = await _httpContextService.GetAppUserAndThrow();
                var hasFeedback = await _feedbackService.HasFeedback(learner.Id, learningModuleId);
                return hasFeedback;
            
        }
    }
}
