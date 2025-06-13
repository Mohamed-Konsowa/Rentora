
using FluentValidation;
using Rentora.Application.Features.Account.Queries.Models;

namespace Rentora.Application.Features.Account.Queries.Validators
{
    public class CheckIfUserNameExistsValidator : AbstractValidator<CheckIfUserNameExistsQuery>
    {
        public CheckIfUserNameExistsValidator()
        {
            Apply();
        }

        public void Apply()
        {
            RuleFor(e => e.UserName)
                .NotEmpty().WithMessage("User Name is required.");
        }
    }
}
