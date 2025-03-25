using Rentora.Application.DTOs.Authentication;

namespace Rentora.Application.IServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(string id);
        Task<bool> CheckIfEmailExists(string email);
        Task<bool> CheckIfUserNameExists(string email);
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
