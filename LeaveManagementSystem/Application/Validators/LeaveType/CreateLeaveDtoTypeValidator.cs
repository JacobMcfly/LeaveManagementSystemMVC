// Application.Validators.LeaveType.CreateLeaveTypeValidator.cs
using FluentValidation;
using LeaveManagementSystem.Application.DTOs.LeaveType;

public class CreateLeaveTypeDtoValidator : AbstractValidator<CreateLeaveTypeDto>
{
    public CreateLeaveTypeDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El Valor es requerido")
            .MaximumLength(150);

        RuleFor(x => x.NumberOfDays)
            .GreaterThan(0).WithMessage("Debe ser mayor a 0.");
    }
}
