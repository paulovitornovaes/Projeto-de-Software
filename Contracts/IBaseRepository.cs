using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Iduff.Contracts;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> ListAllAsync();
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
    IQueryable<T> GetAll(Expression<Func<T, bool>> filter);
    Task<T> GetByIdAsync(int id);
    Task<T> GetByAsync(Expression<Func<T, bool>> filter);
    Task<T> CreateAsync(T t);
    T Create(T t);
    Task SaveAsync();
    Task<T> CreateAndSaveAsync(T t);
    T Update(T t);
    Task AddRangeAsync(List<T> t);
    Task UpdateAndSaveAsync(T t);
    Task DeleteAsync(int id);
    IQueryable<T> GetQueryable();
    IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
    IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    Task<DateTime> GetMaxDtAtualizacao();
}