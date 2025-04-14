using LeaveManagementSystem.Application.Common;
using LeaveManagementSystem.Application.Interfaces;
using LeaveManagementSystem.Application.UseCases;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Enums;
using LeaveManagementSystem.Domain.Interfaces;

namespace LeaveManagementSystem.Application.Services
{
    public class LeaveTypeService : GenericService<LeaveType>, ILeaveTypeService
    {
        private readonly EnsureLeaveTypeIsUnique _isUniqueUseCase;
        public LeaveTypeService(ILeaveTypeRepository repository) : base(repository)
        {
            _isUniqueUseCase = new EnsureLeaveTypeIsUnique(repository);
        }

        public override async Task<GenericServiceResult<LeaveType>> AddAsync(LeaveType data)
        {
            var validation = await _isUniqueUseCase.ExecuteAsync(data);
            if (validation is not null) return validation;

            var result = await base.AddAsync(data);
            return GenericServiceResult<LeaveType>.Ok(result.Data!);
        }

        public override async Task<GenericServiceResult<LeaveType>> UpdateAsync(LeaveType data)
        {
            var validation = await _isUniqueUseCase.ExecuteAsync(data, excludeCurrent: true);
            if (validation is not null) return validation;

            await base.UpdateAsync(data);
            return GenericServiceResult<LeaveType>.Ok(data);
        }
    }
}
