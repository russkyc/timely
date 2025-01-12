using FluentAssertions;
using Russkyc.Timely.Utilities;

namespace Russkyc.Timely.Tests;

public class TimeDiffExtensionsTests
{
    public static TheoryData<DateTime, DateTime, int, int> TimeDiffTestCases = new()
    {
        {
            new DateTime(2025, 1, 12, 8,0,0),
            new DateTime(2025, 1, 12, 12,0,0),
            4,
            0
        },
        {
            new DateTime(2025, 1, 12, 1,35,0),
            new DateTime(2025, 1, 12, 4,15,0),
            2,
            40
        },
        {
            new DateTime(2025, 1, 12, 8,30,0),
            new DateTime(2025, 1, 12, 9,30,0),
            1,
            0
        }
    };

    [Theory, MemberData(nameof(TimeDiffTestCases))]
    void GetTimeDiffTests(DateTime target, DateTime reference, int hours, int minutes)
    {
        // Setup
        var timeDiff = target.CalculateDiffFrom(reference);
        var timeDiffHours = timeDiff.Hours;
        var timeDiffMinutes = timeDiff.Minutes;
        
        // Assert
        timeDiffHours.Should().Be(hours);
        timeDiffMinutes.Should().Be(minutes);
    }
}