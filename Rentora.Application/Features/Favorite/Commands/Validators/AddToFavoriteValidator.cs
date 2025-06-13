using FluentValidation;
using Rentora.Application.Features.Favorite.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Commands.Validators
{
    public class AddToFavoriteValidator : AbstractValidator<AddToFavoriteCommand>
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public AddToFavoriteValidator(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required!")
                .MustAsync(async (key, can) => await _userService.GetUserByIdAsync(key.ToString()) is not null)
                .WithMessage("User not found!");

            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!")
                .MustAsync(async (key, can) => await _productService.GetProductByIdAsync((int)key) is not null)
                .WithMessage("Product not found!");
        }
    }
}
