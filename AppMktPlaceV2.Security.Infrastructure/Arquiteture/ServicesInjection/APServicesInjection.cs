#region REFERENCES
using AppMktPlaceV2.Security.Infrastructure.Arquiteture.RepositoryInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using AppMktPlaceV2.Security.Domain.Connector;
using AppMktPlaceV2.Security.Domain.Interfaces.Data;
using AppMktPlaceV2.Security.Domain.Servies.User;
using AppMktPlaceV2.Security.Domain.Interfaces.Services.User;
using AppMktPlaceV2.Security.Domain.Interfaces.Common;
using AppMktPlaceV2.Security.Domain.Interfaces.Services.Log;
#endregion

namespace AppMktPlaceV2.Security.Infrastructure.Arquiteture.ServicesInjection
{
    public static class APServicesInjection
    {
        #region ADD SERVICES
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            return services
                .RemoveAll<IHttpMessageHandlerBuilderFilter>()
                .RegisterServices()
                .RegisterRepositories()
                .AddServiceNoDependency();
        }
        #endregion

        #region REGFISTER SERVICES
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // DAPPER
            services.AddScoped<APConnector>();
            services.AddTransient<IAPDWork, APWork>();

            // SERVICES
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
        #endregion

        #region ADD SERVICE NO DEPENDENCY
        private static IServiceCollection AddServiceNoDependency(this IServiceCollection services)
        {
            var implementationsType = typeof(APServicesInjection).Assembly.GetTypes()
                .Where(t => typeof(IService).IsAssignableFrom(t) && t.BaseType != null);

            foreach (var item in implementationsType)
            {
                services.AddScoped(item);
            }

            return services;

        }
        #endregion
    }
}
