using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ForFutureSobes.Filters
{
    public class PriorityParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name == "priority")
            {
                parameter.Schema.Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("Low"),
                    new OpenApiString("Medium"),
                    new OpenApiString("High")
                };
            }
        }
    }
}
