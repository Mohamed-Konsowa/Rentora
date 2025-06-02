using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rentora.Application.DTOs.Account;
using Rentora.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Rentora.Application.IRepositories;
using Rentora.Application.IServices;
using Rentora.Application.Helpers;
using Rentora.Application.Features.Account.Commands.Models;
using AutoMapper;

namespace Rentora.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly JWT _jwt;

        public UserService(IUnitOfWork unitOfWork, 
            IImageService imageService, IMapper mapper, IOptions<JWT> jwt)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
            _jwt = jwt.Value;
        }
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            var allusers = await _unitOfWork.users.GetAll();
            return allusers;
        }
        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _unitOfWork.users.GetById(id);
            return user;
        }
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.users.GetByEmail(email);
        }
        public async Task<bool> CheckIfEmailExists(string email)
        {
            return await _unitOfWork.users.GetByEmail(email) is not null;
        }
        public async Task<bool> CheckIfUserNameExists(string username)
        {
            return await _unitOfWork.users.GetByName(username) is not null;
        }
        public async Task<bool> CheckIfNationalIDExists(string nationalID)
        {
            return await _unitOfWork.users.GetByNationalID(nationalID) is not null;
        }

        public async Task<bool> CheckIfPhoneNumberExists(string phoneNumber)
        {
            return await _unitOfWork.users.GetByPhoneNumber(phoneNumber) is not null; ;
        }

        public async Task<(bool, Dictionary<string, List<string>>)> RegisterAsync(RegisterCommand model)
        {
            var errors = new Dictionary<string, List<string>>();

            var ProfileImage = await _imageService.UploadImageAsync(model.ProfileImage);// await GoogleDriveService.UploadImageAsync(model.ProfileImage);// await CommonUtils.ConvertImageToBase64(model.ProfileImage);
            var IDImageFront = await _imageService.UploadImageAsync(model.IDImageFront);// await GoogleDriveService.UploadImageAsync(model.IDImageFront);// await CommonUtils.ConvertImageToBase64(model.IDImageFront);
            var IDImageBack = await _imageService.UploadImageAsync(model.IDImageBack);// await GoogleDriveService.UploadImageAsync(model.IDImageBack);// await CommonUtils.ConvertImageToBase64(model.IDImageBack);

            var user = _mapper.Map<ApplicationUser>(model);
            user.ProfileImage = ProfileImage;
            user.IDImageFront = IDImageFront;
            user.IDImageBack = IDImageBack;

            var result = await _unitOfWork.users.Create(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errors[$"{error.Code}"] = new() { $"{error.Code}:" + $" {error.Description}" };
                }
                return (false, errors);
            }
            await _unitOfWork.users.AddRole(user, "User");

            return (true, null);
        }
        public async Task<AuthModel> LoginAsync(LoginCommand model)
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
            authModel.ProfileImage = user.ProfileImage;
            authModel.EmailConfirmed = user.EmailConfirmed;
            return authModel;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _unitOfWork.users.GeneratePasswordResetTokenAsync(user);
        }
        public async Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            return await _unitOfWork.users.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<bool> UpdateProfileImageAsync(UpdateProfileImageCommand model)
        {
            var profileImage = await _imageService.UploadImageAsync(model.Image);
            var user = await _unitOfWork.users.GetById(model.UserId);
            var result = await _imageService.DeleteImageAsync(user.ProfileImage);
            user.ProfileImage = profileImage;
            await _unitOfWork.Save();
            return true;
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
        public async Task<(bool,string)> AddRoleAsync(AddRoleCommand model)
        {
            var user = await _unitOfWork.users.GetById(model.UserId);
            var role = await _unitOfWork.users.RoleExists(model.Role);
            if (user is null || !role)
                return (false, "Invalid user Id or role name");
            if (await _unitOfWork.users.IsInRole(user, model.Role))
                return (false, "Role assigned to user already");

            var result = await _unitOfWork.users.AddRole(user, model.Role);
            if(!result.Succeeded) return (false,  "Something went wrong");

            return (true, "Success");
        }

        public async Task<bool> UpdateProfileAsync(UpdateProfileCommand model)
        {
            var user = await _unitOfWork.users.GetById(model.Id.ToString());
            if (user is null) return false;
            _mapper.Map(model, user);
            await _unitOfWork.Save();
            return true;
        }
    }
}
