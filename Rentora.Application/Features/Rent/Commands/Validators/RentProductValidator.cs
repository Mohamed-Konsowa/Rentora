using FluentValidation;
using Rentora.Application.Features.Rent.Commands.Models;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class RentProductValidator : AbstractValidator<RentProductCommand>
    {

        public RentProductValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!");

            RuleFor(x => x.ApplicationUserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required.");

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
