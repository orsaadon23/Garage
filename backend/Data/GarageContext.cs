using Microsoft.EntityFrameworkCore;
using Models;

public class GarageContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; } // Representing the Vehicles table

    public GarageContext(DbContextOptions<GarageContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Example Fluent API configuration
        modelBuilder.Entity<Vehicle>().HasKey(v => v.Id);
    }
}
