
using FluentValidation;
using Rentora.Application.Features.Account.Queries.Models;

namespace Rentora.Application.Features.Account.Queries.Validators
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            Apply();
        }

        public void Apply()
        {
            RuleFor(e => e.UserId)
                .NotEmpty().WithMessage("User Id is required.");
        }
    }
}
