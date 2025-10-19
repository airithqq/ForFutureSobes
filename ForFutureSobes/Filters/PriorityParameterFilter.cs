using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ForFutureSobes.API.Filters
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
                    new OpenApiString("Middle"),
                    new OpenApiString("High")
                };
            }
        }
    }
}
