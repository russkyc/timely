using System.ComponentModel.DataAnnotations;

namespace Russkyc.Timely.Models;

public class TimeEntry
{
    [Key]
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
}