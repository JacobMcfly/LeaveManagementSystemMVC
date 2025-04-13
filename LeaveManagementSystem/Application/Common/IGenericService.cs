using System;

namespace LeaveManagementSystem.Application.Common;

public interface IGenericService<T>
{
    Task<T?> GetByIdAsync(long id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task TerminateAsync(long id);
    Task<IEnumerable<T>> GetAllIncludingTerminatedAsync();
}
