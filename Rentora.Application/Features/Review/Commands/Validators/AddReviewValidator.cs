using FluentValidation;
using Rentora.Application.Features.Review.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Commands.Validators
{
    public class AddReviewValidator : AbstractValidator<AddOrUpdateReviewCommand>
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public AddReviewValidator(IReviewService reviewService, IUserService userService, IProductService productService)
        {
            _reviewService = reviewService;
            _userService = userService;
            _productService = productService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.UserId)
                .MustAsync(async (key, can) => await _userService.GetUserByIdAsync(key.ToString()) is not null)
                .WithMessage("User not found!");

            RuleFor(r => r.ProductId)
                .MustAsync(async (key, can) => await _productService.GetProductByIdAsync(key) is not null)
                .WithMessage("Product not found!");

            RuleFor(r => r.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rate must between 1.0 and 5.0!");

            RuleFor(r => r.Comment)
                .NotEmpty().WithMessage("Comment is required!");
        }
    }
}
