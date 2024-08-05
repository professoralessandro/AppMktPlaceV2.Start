namespace AppMktPlaceV2.Start.Domain.Interfaces.Repository.PostgreBase
{
    public interface IRepositoryPostgreBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task UpdateAsync(TEntity obj);

        Task RemoveAsync(TEntity obj);

        void Add(TEntity obj);

        void Dispose();
    }
}
