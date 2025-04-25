using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Application.Helpers;

namespace Rentora.Persistence.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<IdentityResult> AddRole(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, "User");
        }

        public async Task<bool> CheckPassword(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task<IdentityResult> Create(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public Task<ApplicationUser?> GetByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser?> GetByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        public async Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var result =  await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
        public async Task<IList<Claim>> GetClaims(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsInRole(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> RoleExists(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
    }
}
