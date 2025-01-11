using System.ComponentModel.DataAnnotations;
using Russkyc.Timely.Models.Types;

namespace Russkyc.Timely.Models.Entities;

public class TimeEntry
{
    [Key]
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public EntryType Type { get; set; }
}