namespace Russkyc.Timely.Models.Events;

public class SetShowTimerSecondsEvent
{
    public SetShowTimerSecondsEvent(bool show)
    {
        Show = show;
    }
    
    public bool Show { get; set; }
}