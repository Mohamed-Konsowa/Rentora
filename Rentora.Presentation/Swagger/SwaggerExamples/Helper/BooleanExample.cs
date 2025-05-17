using Rentora.Application.Base;
using Swashbuckle.AspNetCore.Filters;

namespace Rentora.Presentation.Swagger.SwaggerExamples.Helper
{
    public class BooleanExample : IMultipleExamplesProvider<Response<bool>>
    {
        public IEnumerable<SwaggerExample<Response<bool>>> GetExamples()
        {
            yield return SwaggerExample.Create("true", ResponseHandler.Success(true));
            yield return SwaggerExample.Create("false", ResponseHandler.Success(false));
        }
    }
}
