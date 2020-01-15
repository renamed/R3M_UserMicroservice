
using Microsoft.Extensions.DependencyInjection;
using R3M_User_ExternalServices;
using R3M_User_Service;
using R3M_User_Service.Interfaces;

namespace R3M_User_Services
{
    public static class ServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IUsuarioService, UsuarioService>();

            services.ConfigureExternalServices();
        }
    }
}