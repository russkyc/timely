using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using Timely.Models;

namespace Timely.Services.Data;

public class ShiftManager
{
    private Shift? _activeShift;
    
    public ShiftManager()
    {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();
    }

    public async Task AddTimeRecord(Shift shift)
    {
        await using var db = new AppDbContext();
        await db.Shifts.AddAsync(shift);
        await db.SaveToCacheAsync();
    }
    
    public async Task<IEnumerable<Shift>?> GetTimeRecords()
    {
        await using var db = new AppDbContext();
        return await db.Shifts.Where(shift => !shift.Active).ToListAsync();
    }

    public async Task ClearTimeRecords()
    {
        await using var db = new AppDbContext();
        await db.Shifts.AsQueryable().ExecuteDeleteAsync();
        await db.SaveToCacheAsync();
    }

    public async Task<Shift?> GetCurrentShift()
    {
        if (_activeShift is not null) return _activeShift;
        
        await using var db = new AppDbContext();
        _activeShift = await db.Shifts.FirstOrDefaultAsync(shift => shift.Active);

        return _activeShift;
    }
    
    public async Task AddShift(TimeSpan start, TimeSpan end, DateTime? date = null)
    {
        
        var shift = new Shift()
        {
            ShiftStart = start,
            ShiftEnd = end,
            Date = date ?? DateTime.Today
        };

        await using var db = new AppDbContext();
        await db.Shifts.AddAsync(shift);
        await db.SaveToCacheAsync();

    }
    
    public async Task StartShift()
    {
        if (_activeShift is not null) return;
        
        var shift = new Shift()
        {
            Date = DateTime.Today,
            ShiftStart = DateTime.Now.TimeOfDay,
            Active = true
        };
        
        await using var db = new AppDbContext();
        var entry = await db.Shifts.AddAsync(shift);
        await db.SaveToCacheAsync();

        _activeShift = entry.Entity;

    }

    public async Task EndShift()
    {
        await GetCurrentShift();
        
        await using var db = new AppDbContext();
        if (_activeShift is null) return;

        _activeShift.Active = false;
        _activeShift.ShiftEnd = DateTime.Now.TimeOfDay;
        
        db.Shifts.Update(_activeShift);
        await db.SaveToCacheAsync();

        _activeShift = null;
    }
}