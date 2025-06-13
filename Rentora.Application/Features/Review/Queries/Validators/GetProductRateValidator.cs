using FluentValidation;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Queries.Validators
    {
        public class GetProductRateValidator : AbstractValidator<GetProductRateQuery>
        {
            private readonly IProductService _productService;

            public GetProductRateValidator(IProductService productService)
            {
                _productService = productService;
                ApplyValidationRules();
            }
            public void ApplyValidationRules()
            {
                RuleFor(r => r.ProductId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Product Id is required!")
                    .MustAsync(async (key, can) => await _productService.GetProductByIdAsync((int)key) is not null)
                    .WithMessage("Product not found!");

            }
        }
    }