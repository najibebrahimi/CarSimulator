using CarSimulator.Items.Enums;
using CarSimulator.Items.Warnings;

namespace CarSimulator.Tests;

public class CarWarningTest
{
    [Fact]
    public void Test_CarWarning_Constructor()
    {
        Assert.Throws<ArgumentException>(() => new CarWarningManager(nonCriticalWarningLevel: 2, criticalWarningLevel: 2));
        Assert.Throws<ArgumentException>(() => new CarWarningManager(nonCriticalWarningLevel: 2, criticalWarningLevel: 4));

        var warningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);
        Assert.Equal(4, warningManager.NonCriticalWarningLevel);
        Assert.Equal(2, warningManager.CriticalWarningLevel);
    }

    [Fact]
    public void Test_CarWarning()
    {
        var warningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);

        var warningState = warningManager.GetWarningState(-1);
        Assert.Equal(WarningState.Critical, warningState);

        warningState = warningManager.GetWarningState(2);
        Assert.Equal(WarningState.Critical, warningState);

        warningState = warningManager.GetWarningState(4);
        Assert.Equal(WarningState.NonCritical, warningState);

        warningState = warningManager.GetWarningState(5);
        Assert.Equal(WarningState.None, warningState);
    }
}
