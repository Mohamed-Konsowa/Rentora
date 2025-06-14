﻿using FluentValidation;
using Rentora.Application.Features.Review.Commands.Models;

namespace Rentora.Application.Features.Review.Commands.Validators
{
    public class DeleteReviewValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.reviewId)
                .NotEmpty().WithMessage("Review Id is required!");
        }
    }
}
