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
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required!")
                .MustAsync(async (key, can) => await _userService.GetUserByIdAsync(key.ToString()) is not null)
                .WithMessage("User not found!");

            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!")
                .MustAsync(async (key, can) => await _productService.GetProductByIdAsync((int)key) is not null)
                .WithMessage("Product not found!");

            RuleFor(r => r.Rating)
                .NotEmpty().WithMessage("Rating is required!")
                .InclusiveBetween(1, 5).WithMessage("Rate must between 1.0 and 5.0!");

            RuleFor(r => r.Comment)
                .NotEmpty().WithMessage("Comment is required!");
        }
    }
}
