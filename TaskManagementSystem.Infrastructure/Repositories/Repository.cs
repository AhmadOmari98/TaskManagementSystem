
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface.Repositories;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : DomainEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        //-------------------------------------------------------------**
        #region Query Methods

        public virtual IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<List<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> ListAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<(List<T> Data, int TotalCount)> PageAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            var query = _dbSet.Where(predicate);

            var pagedQuery = query.Skip(pageIndex * pageSize).Take(pageSize);

            int totalCount = await query.CountAsync();
            var data = await pagedQuery.ToListAsync();

            return (data, totalCount);
        }

        public virtual async Task<bool> AnyAsync()
        {
            return await _dbSet.AnyAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
        #endregion

        #region CRUD Methods

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
