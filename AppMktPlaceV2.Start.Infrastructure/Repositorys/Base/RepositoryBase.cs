#region IMPORTS
using AppMktPlaceV2.Start.Domain.Connector;
using AppMktPlaceV2.Start.Domain.Context.SQLServer;
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.Base;
using Dapper;
using Microsoft.EntityFrameworkCore;
#endregion

namespace AppMktPlaceV2.Start.Infrastructure.Repositorys.Base
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        #region ATRIBUTTES
        protected AppDbContext _context;
        protected APConnector _session;
        #endregion

        #region CONSTRUCTOR
        public RepositoryBase(AppDbContext context, APConnector session) { _context = context; _session = session; }
        #endregion

        #region PUBLIC METHOD
        public virtual async Task AddAsync(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }

        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> ReturnListFromQueryAsync<T>(string query, object? param = null)
        {
            return await _session.Connection.QueryAsync<T>(query, param);
        }

        public async Task<T> ReturnObjectSingleFromQueryAsync<T>(string query, object? param = null)
        {
            return await _session.Connection.QueryFirstAsync<T>(query, param);
        }

        public async Task ExecuteQueryAsync(string query, object? param = null)
        {
            await _session.Connection.ExecuteAsync(query, param);
        }
        #endregion
    }
}
