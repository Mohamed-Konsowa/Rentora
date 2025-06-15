using FluentValidation;
using Rentora.Application.Features.Favorite.Queries.Models;

namespace Rentora.Application.Features.Favorite.Queries.Validators
{
    public class GetUserFavoriteItemsPaginatedQueryValidator : AbstractValidator<GetUserFavoriteItemsPaginatedQuery>
    {
        public GetUserFavoriteItemsPaginatedQueryValidator()
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
