using FluentValidation;
using Rentora.Application.Features.Rent.Queries.Models;

namespace Rentora.Application.Features.Rent.Queries.Validators
{
    public class GetUserRentsPaginatedValidator : AbstractValidator<GetUserRentsPaginatedQuery>
    {

        public GetUserRentsPaginatedValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("User ID is required.");

        }
    }
}