﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IHttpContextService _contextService;

    public AuthController(IAuthService authService, IHttpContextService contextService)
    {
        _authService = authService;
        _contextService = contextService;
    }

    /// <summary>
    /// Login with Username and Password
    /// </summary>
    [HttpPost("Login")]
    public async Task<ActionResult<LoginPayloadDto>> Login(UserCredentialDto dto)
    {
        return Ok(await _authService.Login(dto));
    }



    /// <summary>
    /// Signin with google
    /// </summary>
    [HttpPost("GoogleSignIn")]
    public async Task<ActionResult<LoginPayloadDto>> GoogleSignIn(GoogleSignInVM dto)
    {
        return Ok(await _authService.SignInWithGoogle(dto));
    }

    /// <summary>
    /// Signup with Username and Password
    /// </summary>
    [HttpPost("SignUp")]
    public async Task<ActionResult<LoginPayloadDto>> Signup(CreateUserDto dto)
    {
        return Ok(await _authService.Signup(dto));
    }

    /// <summary>
	/// Get Current User
	/// </summary>
    [HttpGet("Me")]
    public async Task<ActionResult<AppUser>> GetMe()
    {
        return Ok(await _authService.GetMe());
    }

    [HttpPut("ChangePassWord")]
    public async Task<ActionResult<AppUser>> ChangePassWord(UserPassword dto)
    {
        var user = await _contextService.GetAppUserAndThrow();
        return Ok(await _authService.ChangeUserPassWord(user, dto));
    }

    [HttpPost("ForgetPassWord")]
    public async Task<ActionResult<AppUser>> forgetPassword(ForgetPasswordDto dto) {
        return Ok(await _authService.ForgetPassword(dto));
    }
    [HttpPost("VerifyOTP")]
    public async Task<ActionResult<AppUser>> verifyOTP(VerifyOTPDto dto)
    {
       
        return Ok(await _authService.VerifyOTP( dto));
    }

}
