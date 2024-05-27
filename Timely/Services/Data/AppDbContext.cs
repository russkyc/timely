using BlazorDB;
using Microsoft.EntityFrameworkCore;
using Timely.Models;

namespace Timely.Services.Data;

public class AppDbContext : BlazorDBContext
{
    public DbSet<Shift> Shifts { get; set; }
}