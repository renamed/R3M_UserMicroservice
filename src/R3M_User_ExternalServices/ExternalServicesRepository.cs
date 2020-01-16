using System;
using Microsoft.Extensions.DependencyInjection;
using R3M_User_ExternalServices.Interfaces;

namespace R3M_User_ExternalServices
{
    public static class ExternalServicesRepository
    {
        public static void ConfigureExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioExternalService, UsuarioExternalService>();
            services.AddScoped<ITokenExternalService, TokenExternalService>();
            services.AddTransient<IDbContext, MariaDbContext>();
        }
    }
}