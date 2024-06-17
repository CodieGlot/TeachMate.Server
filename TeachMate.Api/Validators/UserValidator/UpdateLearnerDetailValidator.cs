using FluentValidation;
using TeachMate.Domain;

namespace TeachMate.Api
{
    public class UpdateLearnerDetailValidator : AbstractValidator<UpdateLearnerDetailDto>
    {
        public UpdateLearnerDetailValidator() {
            RuleFor(x => x.PhoneNumber)
                      .Length(10).WithMessage("Phone number must 10 numbers!!").Must(PhoneNumber => PhoneNumber.StartsWith("0")).WithMessage("Phone number must begin with number 0!!");

        }   
    }
}
