using FluentValidation;
using TeachMate.Domain;

namespace TeachMate.Api.Validators.UserValidator
{
    public class ForgetPassword : AbstractValidator<ForgetPasswordDto>
    {
        public ForgetPassword() {
            RuleFor(x => x.NewPassword)
                    .NotEmpty()
                    .Length(6,15);
            RuleFor(x => x.OTP)
                .NotEmpty();
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty();
        }
    }
}
