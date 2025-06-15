using FluentValidation;
using Rentora.Application.Features.Review.Commands.Models;

namespace Rentora.Application.Features.Review.Commands.Validators
{
    public class AddReviewValidator : AbstractValidator<AddOrUpdateReviewCommand>
    {

        public AddReviewValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required!");

            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!");

            RuleFor(r => r.Rating)
                .NotEmpty().WithMessage("Rating is required!")
                .InclusiveBetween(1, 5).WithMessage("Rate must between 1.0 and 5.0!");

            RuleFor(r => r.Comment)
                .NotEmpty().WithMessage("Comment is required!");
        }
    }
}
