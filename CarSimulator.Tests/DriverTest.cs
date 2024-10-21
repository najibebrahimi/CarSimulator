using CarSimulator.Items;
using CarSimulator.Items.Actions;
using CarSimulator.Items.Enums;
using CarSimulator.Items.Warnings;

namespace CarSimulator.Tests;

public class DriverTest
{
    [Fact]
    public void MaxFatigue_Throws_ArgumentOutOfRangeException()
    {
        var maxFatigueLevel = -5;
        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 1, criticalWarningLevel: 10);
        Assert.Throws<ArgumentOutOfRangeException>(() => new Driver(maxFatigueLevel, driverWarningManager));
    }

    [Fact]
    public void IncreaseFatigue()
    {
        var maxFatigueLevel = 10;
        var nonCriticalWarningLevel = 8;
        var criticalWarningLevel = 10;
        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel, criticalWarningLevel);
        var driver = new Driver(maxFatigueLevel, driverWarningManager);
        var expected = 0;

        Assert.Equal(maxFatigueLevel, driver.MaxFatigueLevel);
        Assert.Equal(expected, driver.CurrentFatigueLevel);
        Assert.Equal(WarningState.None, driver.CurrentWarningState);
        Assert.Equal(nonCriticalWarningLevel, driver.NonCriticalFatigueWarningLevel);
        Assert.Equal(criticalWarningLevel, driver.CriticalFatigueWarningLevel);

        driver.PerformAction(new DriveForward());
        expected++;
        Assert.Equal(expected, driver.CurrentFatigueLevel);

        driver.PerformAction(new TurnLeft());
        expected++;
        Assert.Equal(expected, driver.CurrentFatigueLevel);

        driver.PerformAction(new TurnRight());
        expected++;
        Assert.Equal(expected, driver.CurrentFatigueLevel);

        driver.PerformAction(new Reverse());
        expected++;
        Assert.Equal(expected, driver.CurrentFatigueLevel);

        driver.PerformAction(new Refill());
        expected++;
        Assert.Equal(expected, driver.CurrentFatigueLevel);
    }

    [Fact]
    public void IncreaseFatigue_MaxReached()
    {
        var maxFatigueLevel = 1;
        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 0, criticalWarningLevel: 1);
        var driver = new Driver(maxFatigueLevel, driverWarningManager);
        var expected = 0;

        Assert.Equal(maxFatigueLevel, driver.MaxFatigueLevel);
        Assert.Equal(expected, driver.CurrentFatigueLevel);
        Assert.Equal(WarningState.NonCritical, driver.CurrentWarningState);

        driver.PerformAction(new DriveForward());
        expected++;
        Assert.Equal(expected, driver.CurrentFatigueLevel);
        Assert.Equal(WarningState.Critical, driver.CurrentWarningState);

        driver.PerformAction(new DriveForward());
        Assert.Equal(expected, driver.CurrentFatigueLevel);
        Assert.Equal(WarningState.Critical, driver.CurrentWarningState);
    }

    [Fact]
    public void DecreaseFatigue()
    {
        var maxFatigueLevel = 10;
        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 8, criticalWarningLevel: 10);
        var driver = new Driver(maxFatigueLevel, driverWarningManager);
        var expected = 0;

        Assert.Equal(maxFatigueLevel, driver.MaxFatigueLevel);
        Assert.Equal(expected, driver.CurrentFatigueLevel);

        driver.PerformAction(new DriveForward());
        expected++;
        Assert.Equal(expected, driver.CurrentFatigueLevel);
        
        driver.PerformAction(new Rest());
        expected--;
        Assert.Equal(expected, driver.CurrentFatigueLevel);
    }

    [Fact]
    public void DecreaseFatigue_MinReached()
    {
        var maxFatigueLevel = 10;
        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 8, criticalWarningLevel: 10);
        var driver = new Driver(maxFatigueLevel, driverWarningManager);

        Assert.Equal(maxFatigueLevel, driver.MaxFatigueLevel);
        Assert.Equal(0, driver.CurrentFatigueLevel);

        driver.PerformAction(new Rest());
        Assert.Equal(0, driver.CurrentFatigueLevel);
    }
}