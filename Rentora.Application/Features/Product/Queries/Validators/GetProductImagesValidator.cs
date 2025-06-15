using FluentValidation;
using Rentora.Application.Features.Product.Queries.Models;

namespace Rentora.Application.Features.Product.Queries.Validators
{
    public class GetProductImagesValidator : AbstractValidator<GetProductImagesQuery>
    {

        public GetProductImagesValidator()
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
