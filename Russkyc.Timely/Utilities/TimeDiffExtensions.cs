namespace Russkyc.Timely.Utilities;

public static class TimeDiffExtensions
{
    public static TimeSpan CalculateDiffFrom(this DateTime targetValue, DateTime referenceValue)
    {
        return referenceValue - targetValue;
    }
}