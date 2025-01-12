using Magic.IndexedDb;
using Magic.IndexedDb.SchemaAnnotations;
using Russkyc.Timely.Models.Constants;
using Russkyc.Timely.Utilities;

namespace Russkyc.Timely.Models.Entities;

[MagicTable("TimeEntry", StringValues.IndexedDbStoreId)]
public class TimeEntry
{
    [MagicPrimaryKey("id")]
    public string Id { get; set; }
    [MagicIndex]
    public int Day { get; set; }
    [MagicIndex]
    public int Month { get; set; }
    [MagicIndex]
    public int Year { get; set; }
    [MagicIndex]
    public DateTime AmIn { get; set; }
    [MagicIndex]
    public DateTime AmOut { get; set; }
    [MagicIndex]
    public DateTime PmIn { get; set; }
    [MagicIndex]
    public DateTime PmOut { get; set; }

    [MagicNotMapped]
    public (int hours, int minutes) GetWorkHours()
    {
        var workHours = 0;
        var workMinutes = 0;

        if (AmIn != default && AmOut != default)
        {
            var amShift = AmOut.CalculateDiffFrom(AmIn);
            workMinutes += amShift.Minutes;
            workHours += amShift.Hours;
        }
        if (PmIn != default && PmOut != default)
        {
            var pmShift = PmOut.CalculateDiffFrom(PmIn);
            workMinutes += pmShift.Minutes;
            workHours += pmShift.Hours;
        }
        
        if (workMinutes > 59)
        {
            var hoursOverflow = workMinutes / 60;
            workHours += hoursOverflow;

            var minutesOverflow = workMinutes % 60;
            workMinutes = minutesOverflow;
        }
        
        return (workHours, workMinutes);
    }
    
}