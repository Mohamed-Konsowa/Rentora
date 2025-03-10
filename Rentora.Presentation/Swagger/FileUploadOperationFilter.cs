using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rentora.Presentation.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParam = context.MethodInfo.GetParameters()
                .FirstOrDefault(p => p.ParameterType == typeof(IFormFile));

            if (fileParam != null)
            {
                operation.Parameters.Clear();
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                            {
                                { fileParam.Name, new OpenApiSchema { Type = "string", Format = "binary" } }
                            },
                                Required = new HashSet<string> { fileParam.Name }
                            }
                        }
                    }
                };
            }
        }
    }
}