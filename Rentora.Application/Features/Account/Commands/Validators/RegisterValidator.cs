using FluentValidation;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.Helpers;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Account.Commands.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IUserService _userService;

        public RegisterValidator(IUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.ProfileImage)
                .Must(I => CommonUtils.IsImage(I).Item1)
                .WithMessage(u => CommonUtils.IsImage(u.ProfileImage).Item2);

            RuleFor(x => x.IDImageFront)
                .Must(I => CommonUtils.IsImage(I).Item1)
                .WithMessage(u => CommonUtils.IsImage(u.IDImageFront).Item2);

            RuleFor(x => x.IDImageBack)
                .Must(I => CommonUtils.IsImage(I).Item1)
                .WithMessage(u => CommonUtils.IsImage(u.IDImageBack).Item2);

            RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MinimumLength(3).WithMessage("First Name must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters long.")
                .MaximumLength(20).WithMessage("Last Name must not exceed 20 characters.");

            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("User Name is required.")
                .MinimumLength(3).WithMessage("User Name must be at least 3 characters long.")
                .MaximumLength(20).WithMessage("User Name must not exceed 20 characters.")
                .MustAsync(async (key, can) => !await _userService.CheckIfUserNameExistsAsync(key))
                .WithMessage("User name already exist!");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(40).WithMessage("Email must not exceed 40 characters.")
                .MustAsync(async (key, can) => !await _userService.CheckIfEmailExistsAsync(key))
                .WithMessage("Email already exist!");

            RuleFor(u => u.NationalID)
                .NotEmpty().WithMessage("National ID is required.")
                .Matches(@"^\d{14}$").WithMessage("National ID must be exactly 14 digits.")
                .MustAsync(async (key, can) => !await _userService.CheckIfNationalIDExistsAsync(key))
                .WithMessage("National ID already exist!"); 

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{11}$").WithMessage("Phone number must be exactly 11 digits.")
                .MustAsync(async (key, can) => !await _userService.CheckIfPhoneNumberExistsAsync(key))
                .WithMessage("Phone Number already exist!");

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
