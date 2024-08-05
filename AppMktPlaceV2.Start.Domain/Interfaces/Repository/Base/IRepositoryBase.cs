namespace AppMktPlaceV2.Start.Domain.Interfaces.Repository.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task UpdateAsync(TEntity obj);

        Task RemoveAsync(TEntity obj);

        void Add(TEntity obj);

        Task<IEnumerable<T>> ReturnListFromQueryAsync<T>(string query, object? param = null);

        Task<T> ReturnObjectSingleFromQueryAsync<T>(string query, object? param = null);

        void Dispose();
    }
}
