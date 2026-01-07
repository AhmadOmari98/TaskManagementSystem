using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.API.Swagger
{
    public class AddHeadersOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            // LoggedIn-UserId header
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "LoggedIn-UserId",
                In = ParameterLocation.Header,
                Required = true,
                Description = "Authenticated user identifier",
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Example = new OpenApiInteger(1)
                }
            });

            // LoggedIn-UserRole header
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "LoggedIn-UserRole",
                In = ParameterLocation.Header,
                Required = true,
                Description = "User role (Admin, User)",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(UserRole))
                               .Select(role => new OpenApiString(role))
                               .Cast<IOpenApiAny>()
                               .ToList(),
                    Example = new OpenApiString(UserRole.Admin.ToString())
                }
            });
        }
    }
}
