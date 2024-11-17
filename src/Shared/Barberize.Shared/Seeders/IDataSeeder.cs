namespace Barberize.Shared.Seeders;

public interface IDataSeeder<TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
