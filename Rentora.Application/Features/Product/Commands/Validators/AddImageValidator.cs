using FluentValidation;
using Rentora.Application.Features.Product.Commands.Models;
using Rentora.Application.Helpers;

namespace Rentora.Application.Features.Product.Commands.Validators
{
    public class AddImageValidator : AbstractValidator<AddImageCommand>
    {

        public AddImageValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required!");

            RuleFor(x => x.Image)
                .Must(I => CommonUtils.IsImage(I).Item1)
                .WithMessage(u => CommonUtils.IsImage(u.Image).Item2);

        }

    }
}
