using CarSimulator.Items.Enums;
using CarSimulator.Items.Warnings;

namespace CarSimulator.Tests;

public class DriverWarningTest
{
    [Fact]
    public void Test_DriverWarning_Constructor()
    {
        Assert.Throws<ArgumentException>(() => new DriverWarningManager(nonCriticalWarningLevel: 2, criticalWarningLevel: 2));
        Assert.Throws<ArgumentException>(() => new DriverWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2));

        var warningManager = new DriverWarningManager(nonCriticalWarningLevel: 2, criticalWarningLevel: 4);
        Assert.Equal(2, warningManager.NonCriticalWarningLevel);
        Assert.Equal(4, warningManager.CriticalWarningLevel);
    }

    [Fact]
    public void Test_DriverWarning()
    {
        var warningManager = new DriverWarningManager(nonCriticalWarningLevel: 2, criticalWarningLevel: 4);

        Assert.Equal(2, warningManager.NonCriticalWarningLevel);
        Assert.Equal(4, warningManager.CriticalWarningLevel);

        var warningState = warningManager.GetWarningState(-1);
        Assert.Equal(WarningState.None, warningState);

        warningState = warningManager.GetWarningState(2);
        Assert.Equal(WarningState.NonCritical, warningState);

        warningState = warningManager.GetWarningState(4);
        Assert.Equal(WarningState.Critical, warningState);

        warningState = warningManager.GetWarningState(5);
        Assert.Equal(WarningState.Critical, warningState);
    }
}
