using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Application.Extensions
{
    public static class JsonCollectionExtensions
    {
        public static IMvcBuilder addJson(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém os nomes originais das propriedades
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            return builder;
        }
    }
}
