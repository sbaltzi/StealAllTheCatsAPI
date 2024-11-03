using Microsoft.EntityFrameworkCore;

namespace StealAllTheCatsAPI.Models;

public class StealAlltheCatsDbContext : DbContext
{
    public StealAlltheCatsDbContext(DbContextOptions<StealAlltheCatsDbContext> options)
        : base(options)
    {
    }

    public DbSet<CatEntity> Cats { get; set; } = null!;
    public DbSet<CatEntity> Tags { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<CatEntity>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<CatEntity>()
            .Property(s => s.Created)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<TagEntity>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<TagEntity>()
           .Property(s => s.Created)
           .HasDefaultValueSql("GETUTCDATE()");
    }
}
