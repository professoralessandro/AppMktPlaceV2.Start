#region REFERENCES
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.Log;
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.User;
using AppMktPlaceV2.Start.Infrastructure.Repositorys.Log;
using AppMktPlaceV2.Start.Infrastructure.Repositorys.User;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace AppMktPlaceV2.Start.Infrastructure.Arquiteture.RepositoryInjection
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
