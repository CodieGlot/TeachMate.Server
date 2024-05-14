﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;

namespace TeachMate.Api;
[Authorize(Roles = CustomRoles.Tutor)]
[Route("api/[controller]")]
[ApiController]
public class TutorController : ControllerBase
{
    public async Task<ActionResult<LearningSession>> CreateLearningSession(CreateLearningSessionDto dto)
    {
        throw new NotImplementedException();
    }
}
