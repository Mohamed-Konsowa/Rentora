using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Domain.Models;

namespace Rentora.Application.IServices
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserById(string id);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<bool> CheckIfEmailExists(string email);
        Task<bool> CheckIfUserNameExists(string userName);
        Task<bool> CheckIfNationalIDExists(string nationalID);
        Task<bool> CheckIfPhoneNumberExists(string phoneNumber);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<(bool, Dictionary<string, List<string>>)> RegisterAsync(RegisterCommand model);
        Task<AuthModel> LoginAsync(LoginCommand model);
        Task<bool> UpdateProfileImageAsync(UpdateProfileImageCommand model);
        Task<(bool, string)> AddRoleAsync(AddRoleCommand model);
    }
}
