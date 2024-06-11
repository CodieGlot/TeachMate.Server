using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;


namespace TeachMate.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchTutor _searchTutor;
        private readonly ISearchClass _searchClass;

        public SearchController(ISearchTutor searchTutor, ISearchClass searchClass)
        {
            _searchTutor = searchTutor;
            _searchClass = searchClass;
        }

        /// <summary>
        /// Get Tutor by DisplayName
        /// </summary>
        [Authorize(Roles = CustomRoles.GeneralUser)]
        [HttpGet("Tutor/")]
        public async Task<ActionResult<List<AppUser>>> Search(string? DisplayName)
        {
            try
            {
                var tutor = await _searchTutor.Search(DisplayName);
                return Ok(tutor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Search Learning Modules
        /// </summary>
        [Authorize(Roles = CustomRoles.GeneralUser)]
        [HttpPost("Classes/")]
        public async Task<ActionResult<List<LearningModule>>> SearchClasses([FromBody] SearchClassDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var results = await _searchClass.Search(dto);
                if (results == null || !results.Any())
                {
                    return NotFound("No learning modules found matching the criteria.");
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
