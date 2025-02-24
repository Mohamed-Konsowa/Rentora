using RentoraAPI.Models;

namespace RentoraAPI.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
