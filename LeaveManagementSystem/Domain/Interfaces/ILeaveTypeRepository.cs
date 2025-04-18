using System;
using LeaveManagementSystem.Domain.Entities;

namespace LeaveManagementSystem.Domain.Interfaces;

public interface ILeaveTypeRepository : IBaseRepository<LeaveType>{
    Task<LeaveType?> GetByNameAsync(string name); 
}
