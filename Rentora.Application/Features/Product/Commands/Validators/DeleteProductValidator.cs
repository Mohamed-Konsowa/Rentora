using FluentValidation;
using Rentora.Application.Features.Product.Commands.Models;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {

        public DeleteProductValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!");

        }
    }
}
