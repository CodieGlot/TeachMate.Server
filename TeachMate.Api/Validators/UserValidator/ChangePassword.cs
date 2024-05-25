using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.InformationDto;
using TeachMate.Services;

namespace TeachMate.Api.Validators.UserValidator
{
    public class ChangePassword : AbstractValidator<UserPassword>
    {

       
        public ChangePassword() {
            
            RuleFor(x => x.Old_Password)
                .NotEmpty()
                ;
            RuleFor(x => x.New_Password)
                    .NotEmpty()
                    .Length(6, 20);
            RuleFor(x => x.Confirm_Password)
                .Equal(x => x.Confirm_Password);
        }
        
    }
}
