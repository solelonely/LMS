using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private readonly Guid _currentTenantId;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // For demonstration, use a dummy TenantId.
        // In a real app, inject an ITenantService to get the current request's TenantId.
        _currentTenantId = Guid.Empty;
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Global Query Filters for Multi-tenancy
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _currentTenantId);
        modelBuilder.Entity<Course>().HasQueryFilter(c => c.TenantId == _currentTenantId);

        // Additional configurations (e.g., Table names, Column constraints) go here
    }
}
