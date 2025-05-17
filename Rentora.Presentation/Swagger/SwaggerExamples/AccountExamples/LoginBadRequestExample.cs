using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Swashbuckle.AspNetCore.Filters;

namespace Rentora.Presentation.Swagger.SwaggerExamples.AccountExamples
{
    public class LoginBadRequestExample : IExamplesProvider<Response<string>>
    {
        public Response<string> GetExamples()
        {
            return ResponseHandler.BadRequest<string>("Email or Password is incorrect!");
        }
    }
}
