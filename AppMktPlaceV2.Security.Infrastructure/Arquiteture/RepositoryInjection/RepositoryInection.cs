#region REFERENCES
using AppMktPlaceV2.Security.Domain.Interfaces.Repository.Log;
using AppMktPlaceV2.Security.Domain.Interfaces.Repository.User;
using AppMktPlaceV2.Security.Infrastructure.Repositorys.Log;
using AppMktPlaceV2.Security.Infrastructure.Repositorys.User;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace AppMktPlaceV2.Security.Infrastructure.Arquiteture.RepositoryInjection
{
    public static class RepositoryInection
    {
        #region REGISTER REPOSITORIES
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services.
                AddScoped<IUserRepository, UserRepository>().
                AddScoped<ILogRepository, LogRepository>();
        }
        #endregion
    }
}
