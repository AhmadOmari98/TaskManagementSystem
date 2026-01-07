using System.Linq.Expressions;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Interface.Repositories
{
    public interface IRepository<T> where T : DomainEntity
    {
        IQueryable<T> Query();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAllAsync(Expression<Func<T, bool>> predicate);
        Task<(List<T> Data, int TotalCount)> PageAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        System.Threading.Tasks.Task AddAsync(T entity);
        System.Threading.Tasks.Task SaveAsync();
    }
}
