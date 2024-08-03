namespace AppMktPlaceV2.Security.Domain.Interfaces.Repository.Log
{
    public interface ILogRepository
    {
        #region FIND BY ID
        Task<AppMktPlaceV2.Security.Domain.Entities.Log> GetByIdAsync(Guid logId);
        #endregion

        #region GET ALL ASYNC
        Task<IEnumerable<AppMktPlaceV2.Security.Domain.Entities.Log>> GetAllAsync();
        #endregion

        #region INSERT
        Task AddAsync(AppMktPlaceV2.Security.Domain.Entities.Log model);
        #endregion

        #region UPDATE
        Task UpdateAsync(AppMktPlaceV2.Security.Domain.Entities.Log model);
        #endregion

        #region DELETE
        Task RemoveAsync(AppMktPlaceV2.Security.Domain.Entities.Log model);
        #endregion
    }
}
