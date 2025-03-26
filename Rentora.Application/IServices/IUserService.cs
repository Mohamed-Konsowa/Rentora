using Rentora.Application.DTOs.Account;
using Rentora.Domain.Models;

namespace Rentora.Application.IServices
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserById(string id);
        Task<bool> CheckIfEmailExists(string email);
        Task<bool> CheckIfUserNameExists(string email);
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
