using FluentValidation;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Rent.Commands.Validators
{
    public class ReturnProductValidator : AbstractValidator<ReturnProductCommand>
    {
        private readonly IProductService _productService;

        public ReturnProductValidator(IProductService productService)
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
