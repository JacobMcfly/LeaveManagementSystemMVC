using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Domain.Entities;

public class LeaveType : CoreEntity
{
    public string Name { get; set; } = default!;
    public int NumberOfDays { get; set; }
}   
