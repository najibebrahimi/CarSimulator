using CarSimulator.Items;
using CarSimulator.Items.Actions;
using CarSimulator.Items.Enums;
using CarSimulator.Items.Warnings;

namespace CarSimulator.Tests;

public class CarTest
{
    [Fact]
    public void TankCapacity_Throws_ArgumentOutOfRangeException()
    {
        var tankCapacity = -5;
        var directionManager = new DirectionManager(CardinalDirection.North);
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);

        Assert.Throws<ArgumentOutOfRangeException>(() => new Car(directionManager, tankCapacity, carWarningManager));
    }

    [Fact]
    public void DrivingDirection_DriveForward()
    {
        var tankCapacity = 20;
        var initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);


        var car = new Car(directionManager, tankCapacity, carWarningManager);

        Assert.Equal(CardinalDirection.North, car.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Forward, car.CurrentDrivingDirection);
        Assert.Equal(tankCapacity, car.CurrentGasLevel);

        var action = new DriveForward();
        var canPerformActionResult = car.CanPerformAction(action);
        var actionResult = car.PerformAction(action);
        Assert.True(actionResult.IsSuccess);

        Assert.Equal(CardinalDirection.North, car.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Forward, car.CurrentDrivingDirection);
        Assert.Equal((tankCapacity - 1), car.CurrentGasLevel);
    }

    [Fact]
    public void DrivingDirection_Reverse()
    {
        var tankCapacity = 20;
        var initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);

        var car = new Car(directionManager, tankCapacity, carWarningManager);

        Assert.Equal(CardinalDirection.North, car.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Forward, car.CurrentDrivingDirection);
        Assert.Equal(tankCapacity, car.CurrentGasLevel);

        var actionResult = car.PerformAction(new Reverse());
        Assert.True(actionResult.IsSuccess);

        Assert.Equal(CardinalDirection.South, car.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Reverse, car.CurrentDrivingDirection);
        Assert.Equal((tankCapacity - 1), car.CurrentGasLevel);
    }

    [Fact]
    public void GasLevel_CanDrive_Refill_Rest_Warnings()
    {
        var tankCapacity = 3;
        var expected = tankCapacity;
        var initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);
        var nonCriticalWarningLevel = 2;
        var criticalWarningLevel = 1;
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel, criticalWarningLevel);

        var car = new Car(directionManager, tankCapacity, carWarningManager);

        Assert.Equal(expected, car.CurrentGasLevel);
        Assert.Equal(WarningState.None, car.CurrentWarningState);
        Assert.Equal(nonCriticalWarningLevel, car.NonCriticalGasWarningLevel);
        Assert.Equal(criticalWarningLevel, car.CriticalGasWarningLevel);

        var actionResult = car.PerformAction(new DriveForward());
        expected--;
        Assert.True(actionResult.IsSuccess);
        Assert.Equal(expected, car.CurrentGasLevel);
        Assert.Equal(WarningState.NonCritical, car.CurrentWarningState);

        actionResult = car.PerformAction(new Reverse());
        expected--;
        Assert.True(actionResult.IsSuccess);
        Assert.Equal(expected, car.CurrentGasLevel);
        Assert.Equal(WarningState.Critical, car.CurrentWarningState);

        actionResult = car.PerformAction(new TurnLeft());
        expected--;
        Assert.True(actionResult.IsSuccess);
        Assert.Equal(expected, car.CurrentGasLevel);
        Assert.Equal(WarningState.Critical, car.CurrentWarningState);

        var canPerformActionResult = car.CanPerformAction(new TurnRight());
        Assert.False(canPerformActionResult.IsSuccess);

        actionResult = car.PerformAction(new Refill());
        expected = tankCapacity;
        Assert.True(actionResult.IsSuccess);
        Assert.Equal(expected, car.CurrentGasLevel);
        Assert.Equal(WarningState.None, car.CurrentWarningState);

        actionResult = car.PerformAction(new Rest());
        Assert.True(actionResult.IsSuccess);
        Assert.Equal(expected, car.CurrentGasLevel);
        Assert.Equal(WarningState.None, car.CurrentWarningState);
    }

    [Fact]
    public void Refill_On_Already_Full_Failure()
    {
        var tankCapacity = 3;
        var initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);
        var nonCriticalWarningLevel = 2;
        var criticalWarningLevel = 1;
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel, criticalWarningLevel);

        var car = new Car(directionManager, tankCapacity, carWarningManager);

        var actionResult = car.CanPerformAction(new Refill());
        Assert.False(actionResult.IsSuccess);

        actionResult = car.PerformAction(new Refill());
        Assert.False(actionResult.IsSuccess);
    }
}
