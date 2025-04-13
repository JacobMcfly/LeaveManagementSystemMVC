using LeaveManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Infraestructure.Persistence.Data.Configurations;
public class LeaveTypeConfiguration : CoreEntityConfiguration<LeaveType>
{
    public override void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);
            
        builder.Property(x => x.NumberOfDays).IsRequired();
    }
}

