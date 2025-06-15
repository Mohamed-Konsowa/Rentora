using FluentValidation;
using Rentora.Application.Features.Review.Queries.Models;

namespace Rentora.Application.Features.Review.Queries.Validators
{
    public class GetProductReviewsPaginatedValidator : AbstractValidator<GetProductReviewsPaginatedQuery>
    {

        public GetProductReviewsPaginatedValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!");

        }
    }
}