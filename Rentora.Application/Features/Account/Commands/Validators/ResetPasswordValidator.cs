using FluentValidation;
using Rentora.Application.Features.Account.Commands.Models;

namespace Rentora.Application.Features.Account.Commands.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Reset token is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        }
    }
}
