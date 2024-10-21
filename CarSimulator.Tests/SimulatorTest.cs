using CarSimulator.Items;
using CarSimulator.Items.Actions;
using CarSimulator.Items.Enums;
using CarSimulator.Items.Warnings;

namespace CarSimulator.Tests;

public class SimulatorTest
{
    [Fact]
    public void DriverFatigueLevel_CarGasLevel_Init()
    {
        var maxFatigueLevel = 10;
        var tankCapacity = 20;
        var initialDirection = CardinalDirection.North;

        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 6, criticalWarningLevel: 9);
        var driver = new Driver(maxFatigueLevel, driverWarningManager);
        var directionManager = new DirectionManager(initialDirection);
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);
        var car = new Car(directionManager, tankCapacity, carWarningManager);
        var simulator = new Simulator(car, driver);

        Assert.Equal(tankCapacity, simulator.CarTankCapacity);
        Assert.Equal(maxFatigueLevel, simulator.DriverMaxFatigueLevel);
        Assert.Equal(0, simulator.CurrentDriverFatigueLevel);
        Assert.Equal(initialDirection, simulator.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Forward, simulator.CurrentDrivingDirection);
        Assert.Equal(WarningState.None, simulator.CurrentCarWarningState);
        Assert.Equal(WarningState.None, simulator.CurrentDriverWarningState);
    }
    
    [Fact]
    public void DriverFatigueLevel_CarGasLevel()
    {
        var maxFatigueLevel = 10;
        var tankCapacity = 20;
        var initialDirection = CardinalDirection.North;
        
        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 6, criticalWarningLevel: 9);
        var driver = new Driver(maxFatigueLevel, driverWarningManager);
        var directionManager = new DirectionManager(initialDirection);
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);
        var car = new Car(directionManager, tankCapacity, carWarningManager);
        var simulator = new Simulator(car, driver);

        var action = new DriveForward();
        var canPerformActionResult = simulator.CanPerformAction(action);
        Assert.True(canPerformActionResult.IsSuccess);
        var actionResult = simulator.PerformAction(action);
        Assert.True(actionResult.IsSuccess);

        var expectedFatigue = 1;
        var expectedGasLevel = tankCapacity - 1;

        Assert.Equal(initialDirection, simulator.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Forward, simulator.CurrentDrivingDirection);
        Assert.Equal(expectedFatigue, simulator.CurrentDriverFatigueLevel);
        Assert.Equal(expectedGasLevel, simulator.CurrentCarGasLevel);
    }

    [Fact]
    public void CanPerform_Driver_And_Car()
    {
        var maxFatigueLevel = 3;
        var tankCapacity = 3;
        var expectedGasLevel = tankCapacity;
        var expectedFatigue = 0;
        var initialDirection = CardinalDirection.North;

        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 1, criticalWarningLevel: 2);
        var driver = new Driver(maxFatigueLevel, driverWarningManager);
        var directionManager = new DirectionManager(initialDirection);
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel: 2, criticalWarningLevel: 1);
        var car = new Car(directionManager, tankCapacity, carWarningManager);
        var simulator = new Simulator(car, driver);

        var action = new DriveForward();
        var actionResult = simulator.PerformAction(action);
        expectedFatigue += 1;
        expectedGasLevel -= 1;

        Assert.True(actionResult.IsSuccess);
        Assert.Equal(expectedFatigue, simulator.CurrentDriverFatigueLevel);
        Assert.Equal(WarningState.NonCritical, simulator.CurrentDriverWarningState);

        Assert.Equal(expectedGasLevel, simulator.CurrentCarGasLevel);
        Assert.Equal(WarningState.NonCritical, simulator.CurrentCarWarningState);

        actionResult = simulator.PerformAction(action);
        expectedFatigue += 1;
        expectedGasLevel -= 1;

        Assert.True(actionResult.IsSuccess);
        Assert.Equal(expectedFatigue, simulator.CurrentDriverFatigueLevel);
        Assert.Equal(WarningState.Critical, simulator.CurrentDriverWarningState);

        Assert.Equal(expectedGasLevel, simulator.CurrentCarGasLevel);
        Assert.Equal(WarningState.Critical, simulator.CurrentCarWarningState);
        
        var canPerformActionResult = simulator.CanPerformAction(action);
        Assert.True(canPerformActionResult.IsSuccess);

        actionResult = simulator.PerformAction(action);
        
        canPerformActionResult = simulator.CanPerformAction(action);
        Assert.False(canPerformActionResult.IsSuccess);

        actionResult = simulator.PerformAction(action);
        Assert.False(canPerformActionResult.IsSuccess);
    }
}
