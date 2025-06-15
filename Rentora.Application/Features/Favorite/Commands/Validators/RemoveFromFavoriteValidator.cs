using FluentValidation;
using Rentora.Application.Features.Favorite.Commands.Models;

namespace Rentora.Application.Features.Favorite.Commands.Validators
{
    public class RemoveFromFavoriteValidator : AbstractValidator<RemoveFromFavoriteCommand>
    {

        public RemoveFromFavoriteValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required!");

            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!");
        }
    }
}
