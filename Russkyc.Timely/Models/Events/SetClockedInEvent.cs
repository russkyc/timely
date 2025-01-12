namespace Russkyc.Timely.Models.Events;

public class SetClockedInEvent
{
    public SetClockedInEvent(bool clockedIn)
    {
        ClockedIn = clockedIn;
    }
    public bool ClockedIn { get; set; }
}