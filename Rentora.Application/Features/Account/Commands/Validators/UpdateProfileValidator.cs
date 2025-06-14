﻿using FluentValidation;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Account.Commands.Validators
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        private readonly IUserService _userService;

        public UpdateProfileValidator(IUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required.");

            RuleFor(u => u.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(3).WithMessage("First Name must be at least 3 characters long.")
                .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.");

            RuleFor(u => u.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters long.")
                .MaximumLength(20).WithMessage("Last Name must not exceed 20 characters.");

            
            RuleFor(u => u.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{11}$").WithMessage("Phone number must be exactly 11 digits.")
                .MustAsync(async (command, key, can) => {
                    var user = await _userService.GetUserByIdAsync(command.Id.ToString());
                    var exist = await _userService.CheckIfPhoneNumberExistsAsync(key);
                    if (user != null) return !exist || user.PhoneNumber == key;
                    return !exist;
                }).WithMessage("Phone Number already exist!");

            RuleFor(u => u.Governorate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Governorate is required.")
                .MinimumLength(3).WithMessage("Governorate must be at least 3 characters long.");

            RuleFor(u => u.Town)
                .NotEmpty().WithMessage("Town is required.");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
