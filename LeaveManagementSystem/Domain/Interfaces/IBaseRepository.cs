using System;
using LeaveManagementSystem.Domain.Entities;

namespace LeaveManagementSystem.Domain.Interfaces;

public interface IBaseRepository<T> where T : CoreEntity
{
    Task<T?> GetByIdAsync(long id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task Terminate(long id);
}
