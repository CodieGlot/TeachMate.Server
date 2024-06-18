using FluentValidation;
using TeachMate.Domain;

namespace TeachMate.Api
{
    public class UpdateTutorDetailValidator : AbstractValidator<UpdateTutorDetailDto>
    {
        public UpdateTutorDetailValidator() {
            RuleFor(x => x.PhoneNumber)
                    .Length(10).WithMessage("Phone number must 10 numbers!!").Must(PhoneNumber => PhoneNumber.StartsWith("0")).WithMessage("Phone number must begin with number 0!!");
                   
        }
    }
}
