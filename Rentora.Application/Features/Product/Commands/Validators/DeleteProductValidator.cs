using FluentValidation;
using Rentora.Application.Features.Product.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductService _productService;

        public DeleteProductValidator(IProductService productService)
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
