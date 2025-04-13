using System;
using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Domain.Entities;

public class CoreEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public SystemState State { get; set; } = SystemState.Active;
}
