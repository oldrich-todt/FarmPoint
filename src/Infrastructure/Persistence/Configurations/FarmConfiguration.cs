using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmPoint.Infrastructure.Persistence.Configurations;
public class FarmConfiguration : IEntityTypeConfiguration<Farm>
{
    public void Configure(EntityTypeBuilder<Farm> builder)
    {
        builder.Property(f => f.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(f => f.Name)
            .IsUnique();
    }
}
