using AssetTracker_WebAPI.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker_WebAPI.Data;

/// <summary>
/// The primary database context for the Asset Tracker application.
/// Manages the entity sets and coordinates database operations, including Identity tables.
/// </summary>
public class AssetTrackerDbContext : IdentityDbContext
{
    public AssetTrackerDbContext(DbContextOptions<AssetTrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Asset> Assets { get; set; } = null!;
    public DbSet<Portfolio> Portfolios { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure precision for currency values to ensure financial accuracy
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Quantity)
            .HasPrecision(18, 8);

        modelBuilder.Entity<Transaction>()
            .Property(t => t.PricePerUnit)
            .HasPrecision(18, 2);
    }
}