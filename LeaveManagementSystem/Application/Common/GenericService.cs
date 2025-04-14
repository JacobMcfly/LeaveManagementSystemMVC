using System;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Enums;
using LeaveManagementSystem.Domain.Interfaces;

namespace LeaveManagementSystem.Application.Common;

public class GenericService<T> : IGenericService<T> where T : CoreEntity
{
    private readonly IBaseRepository<T> _repository;

    public GenericService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<GenericServiceResult<T?>> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return GenericServiceResult<T?>.Ok(entity);
    }

    public async Task<GenericServiceResult<IEnumerable<T>>> GetAllAsync()
    {
        var all = await _repository.GetAllAsync();
        var activos = all.Where(e => e.State == SystemState.Active);
        return GenericServiceResult<IEnumerable<T>>.Ok(activos);
    }

    public virtual async Task<GenericServiceResult<T>> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.State = SystemState.Active;

        await _repository.AddAsync(entity);
        return GenericServiceResult<T>.Ok(entity);
    }

    public virtual async Task<GenericServiceResult<T>> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);
        return GenericServiceResult<T>.Ok(entity);
    }

    public async Task<GenericServiceResult<bool>> TerminateAsync(long id)
    {
        await _repository.Terminate(id);
        return GenericServiceResult<bool>.Ok(true);
    }

    public async Task<GenericServiceResult<IEnumerable<T>>> GetAllIncludingTerminatedAsync()
    {
        var all = await _repository.GetAllAsync();
        return GenericServiceResult<IEnumerable<T>>.Ok(all);
    }

}
