namespace AppMktPlaceV2.Start.Domain.Interfaces.Repository.Log
{
    public interface ILogRepository
    {
        #region FIND BY ID
        Task<AppMktPlaceV2.Start.Domain.Entities.Log> GetByIdAsync(Guid logId);
        #endregion

        #region GET ALL ASYNC
        Task<IEnumerable<AppMktPlaceV2.Start.Domain.Entities.Log>> GetAllAsync();
        #endregion

        #region INSERT
        Task AddAsync(AppMktPlaceV2.Start.Domain.Entities.Log model);
        #endregion

        #region UPDATE
        Task UpdateAsync(AppMktPlaceV2.Start.Domain.Entities.Log model);
        #endregion

        #region DELETE
        Task RemoveAsync(AppMktPlaceV2.Start.Domain.Entities.Log model);
        #endregion
    }
}
