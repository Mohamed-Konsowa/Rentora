using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Swashbuckle.AspNetCore.Filters;

namespace Rentora.Presentation.Swagger.SwaggerExamples.AccountExamples
{
    public class LoginOkExample : IExamplesProvider<Response<AuthModel>>
    {
        public Response<AuthModel> GetExamples()
        {
            return ResponseHandler.Success(new AuthModel
            {
                Id = Guid.NewGuid().ToString(),
                Message = "Login successful.",
                IsAuthinticated = true,
                Username = "mkonsowa",
                Email = "mkonsowa@example.com",
                Roles = new List<string> { "Admin", "User" },
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
                ExpireOn = DateTime.UtcNow.AddHours(30),
                ProfileImage = "https://example.com/profile.jpg",
                EmailConfirmed = true
            });
        }
    }
}
