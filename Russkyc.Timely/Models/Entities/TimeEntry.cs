using Magic.IndexedDb;
using Magic.IndexedDb.SchemaAnnotations;
using Russkyc.Timely.Models.Constants;

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
    public int GetWorkHours()
    {
        var workHours = 0;

        if (AmIn != default && AmOut != default)
        {
            var amShift = AmOut - AmIn;
            workHours += amShift.Hours;
        }
        if (PmIn != default && PmOut != default)
        {
            var pmShift = PmOut - PmIn;
            workHours += pmShift.Hours;
        }

        return workHours;
    }
    
    [MagicNotMapped]
    public int GetWorkMinutes()
    {
        var workMinutes = 0;

        if (AmIn != default && AmOut != default)
        {
            var amShift = AmOut - AmIn;
            workMinutes += amShift.Minutes;
        }
        if (PmIn != default && PmOut != default)
        {
            var pmShift = PmOut - PmIn;
            workMinutes += pmShift.Minutes;
        }

        return workMinutes;
    }
}