using Rentora.Application.Base;
using Swashbuckle.AspNetCore.Filters;

namespace Rentora.Presentation.Swagger.SwaggerExamples.Helper
{
    public class ValidationFailedExample : IExamplesProvider<Response<string>>
    {
        public Response<string> GetExamples()
        {
            var response = ResponseHandler.BadRequest<string>(Messages.ValidationFailed);
            response.Errors = new()
            {
                { "field1", new List<string>() { "field1 is required." } },
                { "field2", new List<string>() { "field2 must be positive number." } }
            };
            return response;
        }
    }
}
