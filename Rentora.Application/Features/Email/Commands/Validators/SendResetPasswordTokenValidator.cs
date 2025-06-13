using FluentValidation;
using Rentora.Application.Features.Email.Commands.Models;

namespace Rentora.Application.Features.Email.Commands.Validators
{
    public class SendResetPasswordTokenValidator : AbstractValidator<SendResetPasswordTokenCommand>
    {
        public SendResetPasswordTokenValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
