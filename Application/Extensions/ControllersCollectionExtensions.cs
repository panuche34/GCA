using Application.Interface;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ControllersCollectionExtensions
    {
        public static IServiceCollection addControllers(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddHttpClient();

            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReconciliationSerproService, ReconciliationSerproService>();
            
            services.AddScoped<ISerproService, SerproService>();

            return services;
        }
    }
}
