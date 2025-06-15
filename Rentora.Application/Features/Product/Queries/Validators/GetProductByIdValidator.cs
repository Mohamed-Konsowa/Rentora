using FluentValidation;
using Rentora.Application.Features.Product.Queries.Models;

namespace Rentora.Application.Features.Product.Queries.Validators
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {

        public GetProductByIdValidator()
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
