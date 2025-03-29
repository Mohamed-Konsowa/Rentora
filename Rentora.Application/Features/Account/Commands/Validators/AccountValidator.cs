using FluentValidation;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.IServices;
using Rentora.Domain.Models;

namespace Rentora.Application.Features.Account.Commands.Validators
{
    public class AccountValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IUserService _userService;

        public AccountValidator(IUserService userService)
        {
            ApplyValidationRules();
            _userService = userService;
        }
        public void ApplyValidationRules()
        {
            RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MinimumLength(3).WithMessage("First Name must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters long.")
                .MaximumLength(20).WithMessage("Last Name must not exceed 20 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(40).WithMessage("Email must not exceed 40 characters.");

            RuleFor(u => u.NationalID)
                .NotEmpty().WithMessage("National ID is required.")
                .Matches(@"^\d{14}$").WithMessage("National ID must be exactly 14 digits.");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{11}$").WithMessage("Phone number must be exactly 11 digits.");

            RuleFor(u => u.Governorate)
                .NotEmpty().WithMessage("Governorate is required.")
                .MinimumLength(4).WithMessage("Governorate must be at least 4 characters long.");

            RuleFor(u => u.Town)
                .NotEmpty().WithMessage("Town is required.");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
