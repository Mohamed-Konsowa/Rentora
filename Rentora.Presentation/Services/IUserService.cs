﻿using Rentora.Application.DTOs.Authentication;
using Rentora.Domain.Models;

namespace Rentora.Presentation.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
