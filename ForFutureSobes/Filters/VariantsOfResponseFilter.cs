using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ForFutureSobes.API.Filters
{
    public class VariantsOfResponseFilter : IParameterFilter
    {
       
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name == "variant")
            {
                parameter.Schema.Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("Short"),
                    new OpenApiString("Detailed"),
                };
            }
        }
    }
}



