using FluentValidation;
using Rentora.Application.Features.Rent.Commands.Models;

namespace Rentora.Application.Features.Rent.Commands.Validators
{
    public class ReturnProductValidator : AbstractValidator<ReturnProductCommand>
    {

        public ReturnProductValidator()
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
