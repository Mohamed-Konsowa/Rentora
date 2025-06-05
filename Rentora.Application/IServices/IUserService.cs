using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Domain.Models;

namespace Rentora.Application.IServices
{
    public interface IUserService
    {
        IQueryable<ApplicationUser> GetAllUsers();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<bool> CheckIfEmailExistsAsync(string email);
        Task<bool> CheckIfUserNameExistsAsync(string userName);
        Task<bool> CheckIfNationalIDExistsAsync(string nationalID);
        Task<bool> CheckIfPhoneNumberExistsAsync(string phoneNumber);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<(bool, Dictionary<string, List<string>>)> RegisterAsync(RegisterCommand model);
        Task<AuthModel> LoginAsync(LoginCommand model);
        Task<bool> UpdateProfileImageAsync(UpdateProfileImageCommand model);
        Task<bool> UpdateProfileAsync(UpdateProfileCommand model);
        Task<(bool, string)> AddRoleAsync(AddRoleCommand model);
    }
}
