using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Entities;

namespace ProductManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
            
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            entity.Property(e => e.ProductDate)
                .IsRequired();
            
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450); // For ASP.NET Core Identity UserId
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            entity.Property(e => e.UpdatedAt);

            // Create unique index on ProductDate to ensure uniqueness
            entity.HasIndex(e => e.ProductDate)
                .IsUnique();

            // Create index on UserId for better query performance
            entity.HasIndex(e => e.UserId);
        });
    }
} 