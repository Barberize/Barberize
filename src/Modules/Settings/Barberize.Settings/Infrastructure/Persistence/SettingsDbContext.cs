using Barberize.Settings.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Barberize.Settings.Infrastructure.Persistence;

public class SettingsDbContext : DbContext
{
    public SettingsDbContext(DbContextOptions<SettingsDbContext> options) : base(options) { }

    public DbSet<Parameter> Parameters => Set<Parameter>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("settings");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
