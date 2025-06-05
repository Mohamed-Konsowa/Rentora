using Microsoft.AspNetCore.Identity;
using Rentora.Domain.Models;
using System.Security.Claims;

namespace Rentora.Application.IRepositories
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> GetAll();
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<ApplicationUser?> GetByNameAsync(string name);
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser?> GetByNationalIDAsync(string nationalID);
        Task<ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddRoleAsync(ApplicationUser user, string role);
        Task<IList<string>> GetRoles(ApplicationUser user);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> RoleExistsAsync(string role);
        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
    }
}
