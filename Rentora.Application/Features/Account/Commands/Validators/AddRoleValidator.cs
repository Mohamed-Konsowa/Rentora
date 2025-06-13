using FluentValidation;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Account.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        private readonly IUserService _userService;

        public AddRoleValidator(IUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required.")
                .MustAsync(async (key, can) => await _userService.GetUserByIdAsync(key.ToString()) is not null)
                .WithMessage("User not found!");

            
            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Role is required.");
        }
    }
}
