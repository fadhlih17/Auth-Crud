using Auth_Crud.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth_Crud.Database;

public class AppDbContext : DbContext
{
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Transactional> Transactionals => Set<Transactional>();

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(builder =>
        {
            builder.HasIndex(admin => admin.Email).IsUnique();
        });
    }
}