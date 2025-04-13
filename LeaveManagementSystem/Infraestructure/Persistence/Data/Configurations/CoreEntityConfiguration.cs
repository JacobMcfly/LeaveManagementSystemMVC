using System;
using LeaveManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Infraestructure.Persistence.Data.Configurations;
public abstract class CoreEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : CoreEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.State).IsRequired();
    }
}

