using FluentValidation;
using Rentora.Application.Features.Product.Commands.Models;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class DeleteImageValidator : AbstractValidator<DeleteImageCommand>
    {

        public DeleteImageValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(p => p.ImageId)
                .NotEmpty().WithMessage("Image Id is required.");
        }

    }
}
