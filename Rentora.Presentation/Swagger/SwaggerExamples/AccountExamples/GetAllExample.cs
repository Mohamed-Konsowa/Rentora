﻿using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Swashbuckle.AspNetCore.Filters;

namespace Rentora.Presentation.Swagger.SwaggerExamples.AccountExamples
{
    public class GetAllExample : IExamplesProvider<Response<List<UserDTO>>>
    {
        public Response<List<UserDTO>> GetExamples()
        {
            return ResponseHandler.Success(new List<UserDTO>()
            {
                new UserDTO
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Mohamed",
                    LastName = "Konsowa",
                    Username = "mkonsowa",
                    Email = "mkonsowa@example.com",
                    EmailConfirmed = true,
                    NationalID = "12345678901234",
                    Personal_summary = "Software Developer",
                    PhoneNumber = "01234567890",
                    Governorate = "Cairo",
                    Town = "Nasr City",
                    Address = "123 Main Street",
                    ProfileImage = "https://example.com/profile.jpg"
                },
                new UserDTO
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Mohamed",
                    LastName = "Konsowa",
                    Username = "mkonsowa",
                    Email = "mkonsowa@example.com",
                    EmailConfirmed = true,
                    NationalID = "12345678901234",
                    Personal_summary = "Software Developer",
                    PhoneNumber = "01234567890",
                    Governorate = "Cairo",
                    Town = "Nasr City",
                    Address = "123 Main Street",
                    ProfileImage = "https://example.com/profile.jpg"
                }
            });
        }
    }
}
