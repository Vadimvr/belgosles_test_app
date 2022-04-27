using belgosles_test_app.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace belgosles_test_app.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
        ;
    }
}
