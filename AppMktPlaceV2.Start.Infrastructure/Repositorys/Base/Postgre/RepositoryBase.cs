﻿using AppMktPlaceV2.Start.Domain.Context.Postgre;
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.PostgreBase;
using Microsoft.EntityFrameworkCore;

namespace AppMktPlaceV2.Start.Infrastructure.Repositorys.Base.Postgre
{
    public class RepositoryPostgreBase<TEntity> : IDisposable, IRepositoryPostgreBase<TEntity> where TEntity : class
    {
        protected APContextPostgre _context;

        public RepositoryPostgreBase(APContextPostgre context) { _context = context; }

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
    }
}
