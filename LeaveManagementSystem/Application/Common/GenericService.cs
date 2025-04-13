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

    public async Task<T?> GetByIdAsync(long id)
        => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() {
        var all = await _repository.GetAllAsync();
        return all.Where(e => e.State == SystemState.Active);
    }

    public async Task AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.State = SystemState.Active;
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);
    }

    public async Task TerminateAsync(long id)
        => await _repository.Terminate(id);

    public async Task<IEnumerable<T>> GetAllIncludingTerminatedAsync()
    => await _repository.GetAllAsync();

}
