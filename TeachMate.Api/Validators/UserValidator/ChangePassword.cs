using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api
{
    public class ChangePasswordValidator : AbstractValidator<UserPassword>
    {

       
        public ChangePasswordValidator() {
            
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
