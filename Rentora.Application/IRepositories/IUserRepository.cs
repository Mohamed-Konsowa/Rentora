using Microsoft.AspNetCore.Identity;
using Rentora.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Application.IRepositories
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAll();
        Task<ApplicationUser?> GetById(string id);
        Task<ApplicationUser?> GetByName(string name);
        Task<ApplicationUser?> GetByEmail(string email);
        Task<ApplicationUser?> GetByNationalID(string nationalID);
        Task<ApplicationUser?> GetByPhoneNumber(string phoneNumber);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<IdentityResult> Create(ApplicationUser user, string password);
        Task<IdentityResult> AddRole(ApplicationUser user, string role);
        Task<IList<string>> GetRoles(ApplicationUser user);
        Task<bool> CheckPassword(ApplicationUser user, string password);
        Task<bool> RoleExists(string role);
        Task<bool> IsInRole(ApplicationUser user, string role);
        Task<IList<Claim>> GetClaims(ApplicationUser user);
    }
}
