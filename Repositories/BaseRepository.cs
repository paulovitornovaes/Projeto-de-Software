using System.Linq.Expressions;
using Iduff.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Iduff.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> ListAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.Where(filter).ToListAsync();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
    {
        return _dbSet.Where(filter);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> GetByAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.FindAsync(filter);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public T Create(T entity)
    {
        _dbSet.Add(entity);
        return entity;
    }


    public async Task<T> CreateAndSaveAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public T Update(T entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task UpdateAndSaveAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<T> entity)
    {
        await _dbSet.AddRangeAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
    
    public virtual IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
            query = query.Include(include);
        return query;
    }

    public async Task<DateTime> GetMaxDtAtualizacao()
    {
        var parameter = Expression.Parameter(typeof(T), "p");
        var property = Expression.Property(parameter, "DtAtualizacao");
        var selector = Expression.Lambda<Func<T, DateTime>>(property, parameter);
        return await _dbSet.Select(selector).MaxAsync(); }
}