namespace Timely.Models;

public record Shift
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan? ShiftStart { get; set; }
    public TimeSpan? ShiftEnd { get; set; }
    public bool Active { get; set; }
}