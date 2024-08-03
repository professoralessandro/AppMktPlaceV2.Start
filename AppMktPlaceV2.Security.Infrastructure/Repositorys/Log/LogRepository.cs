#region IMPORTS
using AppMktPlaceV2.Security.Domain.Context.Postgre;
using AppMktPlaceV2.Security.Domain.Interfaces.Repository.Log;
using AppMktPlaceV2.Security.Infrastructure.Repositorys.Base.Postgre;
#endregion IMPORTS

namespace AppMktPlaceV2.Security.Infrastructure.Repositorys.Log
{
    public class LogRepository : RepositoryPostgreBase<AppMktPlaceV2.Security.Domain.Entities.Log>, ILogRepository
    {
        #region IMPORTS
        public LogRepository(APContextPostgre context) : base(context)
        {
            _context = context;
        }
        #endregion IMPORTS
    }
}
