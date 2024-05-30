using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeachMate.Domain;
using BC = BCrypt.Net.BCrypt;

namespace TeachMate.Services;
public class AuthService : IAuthService
{
    private readonly IConfigService _configService;
    private readonly IHttpContextService _httpContextService;
    private readonly IUserService _userService;
    private readonly IGoogleAuthService _googleAuthService;
    private readonly DataContext _context;
    private readonly OtpService _OtpService;
    public AuthService(IUserService userService, IConfigService configService, IHttpContextService httpContextService, IGoogleAuthService googleAuthService, DataContext context, OtpService otpService)
    {
        _userService = userService;
        _configService = configService;
        _httpContextService = httpContextService;
        _googleAuthService = googleAuthService;
        _context = context;
        _OtpService = otpService;
    }
    public async Task<AppUser> GetMe()
    {
        return await _httpContextService.GetAppUserAndThrow();
    }
    public async Task<LoginPayloadDto> Login(UserCredentialDto dto)
    {
        var user = await _userService.GetUserByUsernameOrEmail(dto.Username);

        if (user is null || !BC.Verify(dto.Password, user.Password))
        {
            throw new UnauthorizedException("Username or Password is not correct.");
        }

        return new LoginPayloadDto
        {
            AccessToken = GenerateTokenPayload(user),
            AppUser = user
        };
    }
    public async Task<LoginPayloadDto> Signup(CreateUserDto dto)
    {
        var user = await _userService.GetUserByUsernameOrEmail(dto.Username);

        if (user != null)
        {
            throw new ConflictException("Username or Email already exists.");
        }

        var appUser = new AppUser
        {
            Username = dto.Username,
            DisplayName = dto.Username,
            Password = dto.Password,
            UserRole = dto.UserRole,
        };

        appUser = await _userService.CreateUser(appUser);

        return new LoginPayloadDto
        {
            AccessToken = GenerateTokenPayload(appUser),
            AppUser = appUser
        };
    }
    public async Task<LoginPayloadDto> SignInWithGoogle(GoogleSignInVM model)
    {
        var userInfo = await _googleAuthService.GoogleSignIn(model);

        var appUser = await _userService.GetUserByUsernameOrEmail(userInfo.Email);

        if (appUser == null)
        {
            appUser = new AppUser
            {
                Email = userInfo.Email,
                DisplayName = userInfo.FirstName + " " + userInfo.LastName,
                UserRole = model.UserRole
            };

            appUser = await _userService.CreateUser(appUser);
        }

        return new LoginPayloadDto
        {
            AccessToken = GenerateTokenPayload(appUser),
            AppUser = appUser
        };
    }
    private TokenPayloadDto GenerateTokenPayload(AppUser appUser)
    {
        return new TokenPayloadDto
        {
            ExpiresIn = _configService.GetInt("Jwt:Lifetime:AccessToken"),
            AccessToken = GenerateToken(appUser, AuthToken.AccessToken),
            RefreshToken = GenerateToken(appUser, AuthToken.RefreshToken)
        };
    }
    private string GenerateToken(AppUser appUser, AuthToken tokenType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.UTF8.GetBytes(_configService.GetString("Jwt:SecretKey"));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, _configService.GetString("Jwt:Subject")),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(JwtRegisteredClaimNames.Typ, tokenType.ToString()),
            new Claim("UserId", appUser.Id.ToString()),
            new Claim("Username", appUser.Username ?? ""),
            new Claim("Email", appUser.Email ?? ""),
            new Claim(ClaimTypes.Role, appUser.ToCustomRole())
        };

        var lifetime = tokenType == AuthToken.AccessToken
            ? _configService.GetInt("Jwt:Lifetime:AccessToken")
            : _configService.GetInt("Jwt:Lifetime:RefreshToken");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(lifetime),
            Issuer = _configService.GetString("Jwt:Issuer"),
            Audience = _configService.GetString("Jwt:Audience"),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
        };

        var jwt = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(jwt);
    }
    public async Task<ResponseDto> ChangeUserPassWord(AppUser user, UserPassword dto)
    {
        if (!BCrypt.Net.BCrypt.Verify(dto.Old_Password, user.Password))
        {
            return new ResponseDto("Wrong recent password");
        }
        else if (dto.Old_Password == dto.New_Password)
        {
            return new ResponseDto("Old password and new password must not be the same");

        }
        else if (dto.New_Password != dto.Confirm_Password)
        {
            return new ResponseDto("Confirm password fail");
        }
        dto.New_Password = BCrypt.Net.BCrypt.HashPassword(dto.New_Password);
        user.Password = dto.New_Password;
        await _context.SaveChangesAsync();
        return new ResponseDto("Change successfully");

    }
    public async Task<ResponseDto> ForgetPassword(ForgetPasswordDto dto)
    {
        
        if (!dto.OTP.Equals(_OtpService.GetOtp()))
        {
            return new ResponseDto("Wrong OTP");
        }
        else if (dto.NewPassword != dto.ConfirmPassword)
        {
            return new ResponseDto("Confirm password fail");
        }
        dto.NewPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        var User = await _context.AppUsers.FirstOrDefaultAsync(p => p.Email == _OtpService.GetEmail());
        User.Password = dto.NewPassword;
        await _context.SaveChangesAsync();
        return new ResponseDto("Change successfully");
    }
    public async Task<ResponseDto>Otp()
    {
        string otp = _OtpService.GetOtp();
        
        return new ResponseDto(otp);

    }
}


