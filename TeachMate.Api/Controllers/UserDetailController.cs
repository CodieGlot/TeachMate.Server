using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetailService _information;
        private readonly IHttpContextService _contextService;
        private readonly IUserService _userService;
        public UserDetailController(IUserDetailService information, IHttpContextService contextService, IUserService userService)
        {
            _information = information;
            _contextService = contextService;
            _userService = userService;
        }
        [Authorize(Roles = CustomRoles.Learner)]
        [HttpPut("Learner/AddDetail")]
        public async Task<ActionResult<AppUser>> InformationLearner(AddLearnerDetailDto dto) 
        {
            var user = await _contextService.GetAppUserAndThrow();
            return Ok(await _information.AddLearnerDetail(user, dto));
        }
        [Authorize(Roles = CustomRoles.Tutor)]
        [HttpPut("Tutor/AddDetail")]
        public async Task<ActionResult<AppUser>> InformationTutor(AddTutorDetailDto dto)
        {
            var user = await _contextService.GetAppUserAndThrow();
            return Ok(await _information.AddTutorDetail(user, dto));
        }
        [Authorize(Roles = CustomRoles.Learner)]
        [HttpPut("Learner/UpdateLearnerDetail")]
        public async Task<ActionResult<AppUser>> UpdateLearnerDetail(UpdateLearnerDetailDto dto)
        {
            var user = await _contextService.GetAppUserAndThrow();
            return Ok(await _information.UpdateLearnerDetail(user, dto));
        }
        [Authorize(Roles = CustomRoles.Tutor)]
        [HttpPut("Tutor/UpdateTutorDetail")]
        public async Task<ActionResult<AppUser>> UpdateTutorDetail(UpdateTutorDetailDto dto) {
            var user = await _contextService.GetAppUserAndThrow();
            return Ok(await _information.UpdateTutorDetail(user,dto));
        }

        [HttpGet("GetUserById/{id}")]
         public async Task<ActionResult<AppUser>> GetUserById(Guid id)
        {
            var currentUser = await _contextService.GetAppUser();
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
