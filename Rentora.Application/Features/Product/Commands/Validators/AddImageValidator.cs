using FluentValidation;
using Rentora.Application.Features.Product.Commands.Models;
using Rentora.Application.Helpers;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class AddImageValidator : AbstractValidator<AddImageCommand>
    {
        private readonly IProductService _productService;

        public AddImageValidator(IProductService productService)
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

            RuleFor(x => x.Image)
                .Must(I => CommonUtils.IsImage(I).Item1)
                .WithMessage(u => CommonUtils.IsImage(u.Image).Item2);

        }

    }
}
