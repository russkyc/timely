namespace Timely.Models;

public record Shift
{
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
}