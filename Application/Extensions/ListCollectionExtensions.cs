using Application.Interface;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    internal static class ListCollectionExtensions
    {
        public static IServiceCollection addListCollections(this IServiceCollection services)
        {
            services.AddScoped<IListService, ListService>();

            return services;
        }
    }
}
