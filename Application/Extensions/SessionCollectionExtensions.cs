using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class SessionCollectionExtensions
    {
        public static IServiceCollection addSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(300);
            });
            return services;
        }
    }
}
