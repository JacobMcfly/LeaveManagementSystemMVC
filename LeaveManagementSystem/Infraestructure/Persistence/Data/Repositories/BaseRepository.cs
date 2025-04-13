using System;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Infraestructure.Persistence.Data.Repositories;

public class BaseRepository<T>: IBaseRepository<T> where T : CoreEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Terminate(long id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return;
        
        entity.State = Domain.Enums.SystemState.Terminated;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    
}
