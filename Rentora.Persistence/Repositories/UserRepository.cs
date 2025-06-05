using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Application.Helpers;
using Rentora.Persistence.Data.DbContext;

namespace Rentora.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly JWT _jwt;

        public UserRepository(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext context,
            IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _jwt = jwt.Value;
        }

        public async Task<IdentityResult> AddRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            var users = _userManager.Users.AsNoTracking();
            return users;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser?> GetByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<ApplicationUser?> GetByNationalIDAsync(string nationalID)
        {
            return await _context.Users.Where(u => u.NationalID == nationalID)
                                       .FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Users.Where(u => u.PhoneNumber == phoneNumber)
                                       .FirstOrDefaultAsync();
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
        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
    }
}
