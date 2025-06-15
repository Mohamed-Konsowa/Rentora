using FluentValidation;
using Rentora.Application.Features.Cart.Commands.Models;

namespace Rentora.Application.Features.Cart.Commands.Validators
{
    public class RemoveFromCartValidator : AbstractValidator<RemoveFromCartCommand>
    {
        
        public RemoveFromCartValidator()
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
