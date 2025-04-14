using System;
using LeaveManagementSystem.Application.Common;
using LeaveManagementSystem.Domain.Interfaces;
using LeaveManagementSystem.Domain.Entities;

namespace LeaveManagementSystem.Application.UseCases;

public class EnsureLeaveTypeIsUnique
{
    private readonly ILeaveTypeRepository _repository;

    public EnsureLeaveTypeIsUnique(ILeaveTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<GenericServiceResult<LeaveType>?> ExecuteAsync(LeaveType entity, bool excludeCurrent = false)
    {
        var existing = await _repository.GetByNameAsync(entity.Name);
        if (existing is not null && (!excludeCurrent || existing.Id != entity.Id))
            return GenericServiceResult<LeaveType>.Fail("Ya existe un tipo de permiso con ese nombre.");

        return null;
    }
}
