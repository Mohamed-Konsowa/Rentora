using FluentValidation;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Queries.Validators
{
    public class GetProductReviewsPaginatedValidator : AbstractValidator<GetProductReviewsPaginatedQuery>
    {
        private readonly IProductService _productService;

        public GetProductReviewsPaginatedValidator(IProductService productService)
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