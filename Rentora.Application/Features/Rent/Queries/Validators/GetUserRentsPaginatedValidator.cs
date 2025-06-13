using FluentValidation;
using Rentora.Application.Features.Rent.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Rent.Queries.Validators
{
    public class GetUserRentsPaginatedValidator : AbstractValidator<GetUserRentsPaginatedQuery>
    {
        private readonly IUserService _userService;

        public GetUserRentsPaginatedValidator(IUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .MustAsync(async (Key, can) => await _userService.GetUserByIdAsync(Key.ToString()) is not null)
                .WithMessage("User not found");

        }
    }
}