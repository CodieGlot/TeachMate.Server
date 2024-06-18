using TeachMate.Domain;

namespace TeachMate.Services;
public interface IAuthService
{
    Task<AppUser> GetMe();
    Task<LoginPayloadDto> Login(UserCredentialDto dto);
    Task<LoginPayloadDto> SignInWithGoogle(GoogleSignInVM model);
    Task<LoginPayloadDto> Signup(CreateUserDto dto);
    Task<ResponseDto> ChangeUserPassWord(AppUser user, UserPassword dto);
    Task<ResponseDto> ForgetPassword( ForgetPasswordDto dto);
    Task<ResponseDto> VerifyOTP(VerifyOTPDto dto);

}