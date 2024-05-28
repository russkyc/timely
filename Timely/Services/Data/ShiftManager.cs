using Blazored.LocalStorage;
using Timely.Models;

namespace Timely.Services.Data;

public class ShiftManager
{
    private readonly ILocalStorageService _storage;
    private Shift? _activeShift;
    private List<Shift>? _shifts = new();
    
    public ShiftManager(ILocalStorageService storage)
    {
        _storage = storage;
    }

    public async Task GetData()
    {
        _shifts = await _storage.GetItemAsync<List<Shift>>("shifts");
        _activeShift = await _storage.GetItemAsync<Shift>("activeShift");
    }

    public async Task SaveData()
    {
        await _storage.SetItemAsync("activeShift", _activeShift);
        await _storage.SetItemAsync("shifts", _shifts);
    }

    public async Task AddTimeRecord(Shift shift)
    {
        await GetData();
        
        _shifts ??= new();
        _shifts.Add(shift);

        await SaveData();
    }

    public async Task<Shift?> GetCurrentShift()
    {
        await GetData();
        return _activeShift;
    }
    
    public async Task<IEnumerable<Shift>?> GetTimeRecords()
    {
        await GetData();
        return _shifts;
    }

    public async Task ClearTimeRecords()
    {
        _shifts = null;
        await _storage.RemoveItemAsync("shifts");
        await SaveData();
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
        
        _activeShift = shift;

        await SaveData();
    }

    public async Task EndShift()
    {
        await GetData();
        
        if (_activeShift is null) return;

        _activeShift.Active = false;
        _activeShift.ShiftEnd = DateTime.Now.TimeOfDay;

        _shifts ??= new();
        
        _shifts.Add(_activeShift);
        _activeShift = null;
        
        await SaveData();
    }
}