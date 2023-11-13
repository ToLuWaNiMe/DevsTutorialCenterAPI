using DevsTutorialCenterAPI.Data.Entities;
using System.Linq; // For LINQ operations
using Microsoft.EntityFrameworkCore; // For Entity Framework Core

namespace DevsTutorialCenterAPI.Data.Repositories;

public class Repository : IRepository
{
    protected readonly DevsTutorialCenterAPIContext _context;

    public Repository(DevsTutorialCenterAPIContext context)
    {
        _context = context;
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync<T>(T entities) where T : class
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(T entity) where T : class
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync<T>(T entities) where T : class
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public Task<IQueryable<T>> GetAllAsync<T>() where T : class
    {
        return Task.Run(() => _context.Set<T>().AsQueryable());
    }

    public async Task<T?> GetByIdAsync<T>(string id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IQueryable<T>> GetAllAsync2<T>() where T : class
    {
        var result = await _context.Set<T>().ToListAsync();
        return result.AsQueryable();
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

}