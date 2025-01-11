using Microsoft.EntityFrameworkCore;
using Russkyc.Timely.Models.Entities;

namespace Russkyc.Timely.Services.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<TimeEntry> TimeEntries { get; set; }
}