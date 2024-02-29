using EtaLearning.DataAccess.Data.Entities;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EtaLearning.API;
public class CustomSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(DbClient))
        {
            schema.Type = "object";
            schema.Properties = new Dictionary<string, OpenApiSchema>
                {
                    { "name", new OpenApiSchema { Type = "string" } },
                    { "creationDate", new OpenApiSchema { Type = "string", Format = "date-time" } },
                    // Add properties for smart devices if needed
                };
            schema.Required = new HashSet<string> { "name" }; // Specify required properties
        }
    }

}