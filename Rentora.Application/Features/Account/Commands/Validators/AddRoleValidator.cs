using FluentValidation;
using Rentora.Application.Features.Account.Commands.Models;

namespace Rentora.Application.Features.Account.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {

        public AddRoleValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required.");
            
            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Role is required.");
        }
    }
}
