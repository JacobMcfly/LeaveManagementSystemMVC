using LeaveManagementSystem.Application.Common;
using LeaveManagementSystem.Application.Interfaces;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Interfaces;

namespace LeaveManagementSystem.Application.Services
{
    public class LeaveTypeService : GenericService<LeaveType>, ILeaveTypeService
    {
        public LeaveTypeService(ILeaveTypeRepository repository) : base(repository)
        {
        }
    }
}
