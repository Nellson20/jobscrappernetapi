using JobFinder.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Api.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Job> Jobs => Set<Job>();
}