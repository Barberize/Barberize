using Barberize.Settings.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barberize.Settings.Infrastructure.Persistence.Configurations;

public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Key).IsRequired();
        builder.HasIndex(x => x.Key).IsUnique();
        builder.Property(x => x.DataType).IsRequired().HasConversion<string>();
    }
}
