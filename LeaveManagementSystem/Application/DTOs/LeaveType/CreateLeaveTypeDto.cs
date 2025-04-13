namespace LeaveManagementSystem.Application.DTOs.LeaveType;

public class CreateLeaveTypeDto
{
    public long? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int NumberOfDays { get; set; }
}