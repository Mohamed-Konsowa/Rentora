using FluentValidation;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class RentProductValidator : AbstractValidator<RentProductCommand>
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public RentProductValidator(IUserService userService, IProductService productService)
        {
            _userService = userService;
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

            RuleFor(x => x.ApplicationUserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required.")
                .MustAsync(async (key, can) => await _userService.GetUserByIdAsync(key.ToString()) is not null)
                .WithMessage("User not found!");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start Date is required!");

            RuleFor(x => x.numOfDays)
                .NotNull().WithMessage("Number of Days is required!")
                .GreaterThan(0).WithMessage("Number of Days must be greater than 0.");

            RuleFor(x => x.TotalPrice)
                .NotNull().WithMessage("Total price is required!")
                .GreaterThan(0).WithMessage("Total price must be greater than 0.");

            RuleFor(x => x.RentStatus)
                .NotNull().WithMessage("Rent status is required!");

            RuleFor(x => x.PenaltyFee)
                .NotNull().WithMessage("Penalty Fee is required!");
        }
    }
}
