using Microsoft.Extensions.DependencyInjection;
using R3M_User_App.Interfaces;
using R3M_User_Services;

namespace R3M_User_App
{
    public static class AppExtensions
    {
        public static void ConfigureApp(this IServiceCollection services)
        {
            services.AddSingleton<IUsuarioApp, UsuarioApp>();

            services.ConfigureServices();
        }
    }
}