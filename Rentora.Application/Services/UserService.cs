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

namespace Rentora.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly JWT _jwt;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,
            IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
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
        public async Task<bool> CheckIfEmailExists(string email)
        {
            return await _unitOfWork.users.GetByEmail(email) is not null;
        }
        public async Task<bool> CheckIfUserNameExists(string username)
        {
            return await _unitOfWork.users.GetByName(username) is not null;
        }
        public async Task<(bool, Dictionary<string, List<string>>)> RegisterAsync(RegisterCommand model)
        {
            var errors = new Dictionary<string, List<string>>();
            if (await _unitOfWork.users.GetByEmail(model.Email) is not null)
                errors["Email"] = new() { "Email: already exist!" };
            if (await _unitOfWork.users.GetByName(model.UserName) is not null)
                errors["Username"] = new(){"Username: already exist!"};

            if (!CommonUtils.IsImage(model.ProfileImage).Item1)
                errors["ProfileImage"] = new() { "ProfileImage: Invalid file type. Only JPEG, PNG, and GIF are allowed." };
            
            if (!CommonUtils.IsImage(model.IDImageFront).Item1)
                errors["IDImageFront"] = new() { "IDImageFront: Invalid file type. Only JPEG, PNG, and GIF are allowed." };

            if (!CommonUtils.IsImage(model.IDImageBack).Item1)
                errors["IDImageBack"] = new() { "IDImageBack: Invalid file type. Only JPEG, PNG, and GIF are allowed." };
            if(errors.Count > 0) return (false, errors);

            var profileImage = await _imageService.UploadImageAsync(model.ProfileImage);// await GoogleDriveService.UploadImageAsync(model.ProfileImage);// await CommonUtils.ConvertImageToBase64(model.ProfileImage);
            var IDImageFront = await _imageService.UploadImageAsync(model.IDImageFront);// await GoogleDriveService.UploadImageAsync(model.IDImageFront);// await CommonUtils.ConvertImageToBase64(model.IDImageFront);
            var IDImageBack = await _imageService.UploadImageAsync(model.IDImageBack);// await GoogleDriveService.UploadImageAsync(model.IDImageBack);// await CommonUtils.ConvertImageToBase64(model.IDImageBack);
            
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = model.EmailConfirmed,
                ProfileImage = profileImage,
                IDImageFront = IDImageFront,
                IDImageBack = IDImageBack,
                Personal_summary = model.Personal_summary,
                NationalID = model.NationalID,
                Governorate = model.Governorate,
                Town = model.Town,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
            };
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
        public async Task<AuthModel> GetTokenAsync(LoginCommand model)
        {
            var authModel = new AuthModel();

            var user = await _unitOfWork.users.GetByEmail(model.Email);

            if (user is null || !await _unitOfWork.users.CheckPassword(user, model.Password))
            {
                authModel.Message ="Email or Password is incorrect!";
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
        
    }
}
