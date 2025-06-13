
using FluentValidation;
using Rentora.Application.Features.Account.Queries.Models;

namespace Rentora.Application.Features.Account.Queries.Validators
{
    public class CheckIfEmailExistsValidator : AbstractValidator<CheckIfEmailExistsQuery>
    {
        public CheckIfEmailExistsValidator()
        {
            Apply();
        }

        public void Apply()
        {
            RuleFor(e => e.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
