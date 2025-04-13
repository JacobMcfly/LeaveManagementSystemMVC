using System;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Infraestructure.Persistence.Data.Repositories;
public class LeaveTypeRepository : BaseRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(ApplicationDbContext context) : base(context) { }
}
