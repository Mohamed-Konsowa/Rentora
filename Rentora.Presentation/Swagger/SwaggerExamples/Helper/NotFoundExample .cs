using Rentora.Application.Base;
using Swashbuckle.AspNetCore.Filters;

namespace Rentora.Presentation.Swagger.SwaggerExamples.Helper
{
    public class NotFoundExample : IExamplesProvider<Response<string>>
    {
        public Response<string> GetExamples()
        {
            return ResponseHandler.NotFound<string>();
        }
    }
}
