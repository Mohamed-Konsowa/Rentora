using FluentValidation;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.Helpers;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Account.Commands.Validators
{
    public class UpdateProfileImageValidator : AbstractValidator<UpdateProfileImageCommand>
    {
        IUserService _userService;
        public UpdateProfileImageValidator(IUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Image)
                .Must(I => CommonUtils.IsImage(I).Item1)
                .WithMessage(u => CommonUtils.IsImage(u.Image).Item2);

            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required.")
                .MustAsync(async (key, can) => await _userService.GetUserByIdAsync(key.ToString()) is not null)
                .WithMessage("User not found!");
        }
    }
}
