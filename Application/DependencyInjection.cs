using Application.Extensions;
using Application.Interface;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IMvcBuilder addApplicationMvc(this IMvcBuilder builder, IConfiguration configuration)
        {
            builder.Services.addDbContextFromEntityFramework(configuration);
            builder.addJson();
            builder.Services.addSession();
            builder.Services.addAuthenticationWithJwt();
            //builder.AddRazorRuntimeCompilation();
            
            builder.Services.addListCollections();
            builder.Services.addControllers();
            
            return builder;
        }

        public static IServiceCollection addApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.addDbContextFromEntityFramework(configuration);

            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ISheetService, SheetService>();
            services.AddScoped<IListService, ListService>();

            return services;
        }
    }
}
