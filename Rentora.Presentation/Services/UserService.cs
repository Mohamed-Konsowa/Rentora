using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rentora.Application.DTOs.Authentication;
using Rentora.Persistence.Helpers;
using Rentora.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Rentora.Application.IRepositories;
using Azure.Core;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Rentora.Presentation.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWT _jwt;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _unitOfWork.users.GetByEmail(model.Email) is not null)
                return new AuthModel { Message = "Email already exist!" };
            if (await _unitOfWork.users.GetByName(model.Username) is not null)
                return new AuthModel { Message = "Username already exist!" };

            using var memoryStream = new MemoryStream();
            await model.ProfileImage.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();
            var profileImageBase64 = Convert.ToBase64String(imageBytes);

            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                ProfileImage = profileImageBase64
            };
            var result = await _unitOfWork.users.Create(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel() { Message = errors };
            }
            await _unitOfWork.users.AddRole(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Id = user.Id,
                Email = user.Email,
                ProfileImageBase64 = profileImageBase64,
                ExpireOn = jwtSecurityToken.ValidTo,
                IsAuthinticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _unitOfWork.users.GetByEmail(model.Email);

            if (user is null || !await _unitOfWork.users.CheckPassword(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _unitOfWork.users.GetRoles(user);

            authModel.Id = user.Id;
            authModel.IsAuthinticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpireOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();
            return authModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _unitOfWork.users.GetClaims(user);
            var roles = await _unitOfWork.users.GetRoles(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _unitOfWork.users.GetById(model.UserId);
            var role = await _unitOfWork.users.RoleExists(model.Role);
            if (user is null || !role)
                return "Invalid user Id or role name";
            if (await _unitOfWork.users.IsInRole(user, model.Role))
                return "Role assigned to user already";

            var result = await _unitOfWork.users.AddRole(user, model.Role);
            return !result.Succeeded ? "Something went wrong" : "";
        }
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            var users = await _unitOfWork.users.GetAll();
            return users;
        }

    }
}
