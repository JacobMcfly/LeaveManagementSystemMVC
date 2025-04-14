using System;

namespace LeaveManagementSystem.Application.Common;

public interface IGenericService<T>
{
    Task<GenericServiceResult<T?>> GetByIdAsync(long id);
    Task<GenericServiceResult<IEnumerable<T>>> GetAllAsync();
    Task<GenericServiceResult<T>> AddAsync(T entity);
    Task<GenericServiceResult<T>> UpdateAsync(T entity);
    Task<GenericServiceResult<bool>> TerminateAsync(long id);
    Task<GenericServiceResult<IEnumerable<T>>> GetAllIncludingTerminatedAsync();
}
