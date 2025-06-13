using FluentValidation;
using Rentora.Application.Features.Favorite.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Queries.Validators
{
    public class GetUserFavoriteItemsPaginatedQueryValidator : AbstractValidator<GetUserFavoriteItemsPaginatedQuery>
    {
        private readonly IUserService _userService;
        public GetUserFavoriteItemsPaginatedQueryValidator(IUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(r => r.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("User Id is required!")
                .MustAsync(async (key, can) => await _userService.GetUserByIdAsync(key.ToString()) is not null)
                .WithMessage("User not found!");

            RuleFor(r => r.PageSize)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Page Size Id is required!")
                .Must(key => key > 0)
                .WithMessage("Page Size must be Positive!");

            RuleFor(r => r.PageNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Page Number Id is required!")
                .Must(key => key > 0)
                .WithMessage("Page Number must be Positive!");
        }
    }
}
