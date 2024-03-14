using Blazored.LocalStorage;
using Timely.Models;

namespace Timely.Services.Data;

public class ShiftManager
{
    private Shift? _currentShift;
    private LinkedList<Shift>? _timeRecords;
    private ILocalStorageService _localStorage;

    public ShiftManager(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _timeRecords = new LinkedList<Shift>();
    }

    public async Task GetDataFromLocalStorage()
    {
        _currentShift = await _localStorage.GetItemAsync<Shift?>("current-shift");
        
        var timeRecords = await _localStorage.GetItemAsync<LinkedList<Shift>?>("time-records");
        _timeRecords = timeRecords ?? new LinkedList<Shift>(Enumerable.Empty<Shift>());

        await UpdateTimeRecords();
    }
    
    public async Task UpdateTimeRecords()
    {
        await _localStorage.SetItemAsync("time-records", _timeRecords);
    }

    public async Task AddTimeRecord(Shift shift)
    {
        _timeRecords.AddFirst(shift);
        await UpdateTimeRecords();
        await GetDataFromLocalStorage();
    }
    
    public async Task<LinkedList<Shift>?> GetTimeRecords()
    {
        return await _localStorage.GetItemAsync<LinkedList<Shift>?>("time-records");
    }

    public async Task ClearTimeRecords()
    {
        await _localStorage.RemoveItemAsync("time-records");
        await GetDataFromLocalStorage();
    }

    public async Task<Shift?> GetCurrentShift()
    {
        return await _localStorage.GetItemAsync<Shift>("current-shift");
    }
    
    public async Task StartShift()
    {
        _currentShift ??= new Shift()
        {
            ShiftStart = DateTime.Now
        };
        
        await _localStorage.SetItemAsync("current-shift",_currentShift);
    }

    public async Task EndShift()
    {
        _currentShift.ShiftEnd = DateTime.Now;
        
        await AddTimeRecord(_currentShift);

        await _localStorage.RemoveItemAsync("current-shift");
        await GetDataFromLocalStorage();
    }
}