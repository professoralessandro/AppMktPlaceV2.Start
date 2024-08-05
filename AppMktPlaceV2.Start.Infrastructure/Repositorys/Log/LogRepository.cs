#region IMPORTS
using AppMktPlaceV2.Start.Domain.Context.Postgre;
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.Log;
using AppMktPlaceV2.Start.Infrastructure.Repositorys.Base.Postgre;
#endregion IMPORTS

namespace AppMktPlaceV2.Start.Infrastructure.Repositorys.Log
{
    public class LogRepository : RepositoryPostgreBase<AppMktPlaceV2.Start.Domain.Entities.Log>, ILogRepository
    {
        #region IMPORTS
        public LogRepository(APContextPostgre context) : base(context)
        {
            _context = context;
        }
        #endregion IMPORTS
    }
}
