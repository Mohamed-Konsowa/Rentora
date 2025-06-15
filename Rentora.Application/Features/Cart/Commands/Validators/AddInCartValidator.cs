using FluentValidation;
using Rentora.Application.Features.Cart.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Cart.Commands.Validators
{
    public class AddInCartValidator : AbstractValidator<AddInCartCommand>
    {
        public AddInCartValidator()
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
