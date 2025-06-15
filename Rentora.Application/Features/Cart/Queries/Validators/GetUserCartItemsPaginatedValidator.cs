using FluentValidation;
using Rentora.Application.Features.Cart.Queries.Models;

namespace Rentora.Application.Features.Cart.Queries.Validators
{
    public class GetUserCartItemsPaginatedValidator : AbstractValidator<GetUserCartItemsPaginatedQuery>
    {
        public GetUserCartItemsPaginatedValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required!");

            RuleFor(r => r.PageSize)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Page Size Id is required!")
                .Must(key => key > 0)
                .WithMessage("Page Size must be Positive!");

            RuleFor(r => r.PageNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Page Number Id is required!")
                .Must(key => key > 0)
                .WithMessage("Page Number must be Positive!");
        }
    }
}
