using CarSimulator.Items;
using CarSimulator.Items.Enums;

namespace CarSimulator.Tests;

public class DirectionManagerTest
{
    [Fact]
    public void InitialDirection()
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        Assert.Equal(CardinalDirection.North, directionManager.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Forward, directionManager.CurrentDrivingDirection);
    }

    [Fact]
    public void TurnLeft_DrivingForward()
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.West, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.South, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.East, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.North, directionManager.CurrentCardinalDirection);
    }

    [Fact]
    public void TurnRight_DrivingForward() 
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.East, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.South, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.West, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.North, directionManager.CurrentCardinalDirection);
    }

    [Fact]
    public void DriveForward()
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        directionManager.UpdateDirection(ActionType.DriveForward);
        Assert.Equal(CardinalDirection.North, directionManager.CurrentCardinalDirection);
    }

    [Fact]
    public void Reverse_Then_Forward()
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        directionManager.UpdateDirection(ActionType.DriveReverse);
        Assert.Equal(DrivingDirection.Reverse, directionManager.CurrentDrivingDirection);
        Assert.Equal(CardinalDirection.South, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.DriveForward);
        Assert.Equal(CardinalDirection.North, directionManager.CurrentCardinalDirection);
        Assert.Equal(DrivingDirection.Forward, directionManager.CurrentDrivingDirection);
    }

    [Fact]
    public void Reverse_Then_Reverse()
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        directionManager.UpdateDirection(ActionType.DriveReverse);
        Assert.Equal(DrivingDirection.Reverse, directionManager.CurrentDrivingDirection);
        Assert.Equal(CardinalDirection.South, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.DriveReverse);
        Assert.Equal(DrivingDirection.Reverse, directionManager.CurrentDrivingDirection);
        Assert.Equal(CardinalDirection.South, directionManager.CurrentCardinalDirection);
    }

    [Fact]
    public void TurnLeft_Reversing()
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        directionManager.UpdateDirection(ActionType.DriveReverse);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.West, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.North, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.East, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnLeft);
        Assert.Equal(CardinalDirection.South, directionManager.CurrentCardinalDirection);
    }

    [Fact]
    public void TurnRight_Reversing()
    {
        CardinalDirection initialDirection = CardinalDirection.North;
        var directionManager = new DirectionManager(initialDirection);

        directionManager.UpdateDirection(ActionType.DriveReverse);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.East, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.North, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.West, directionManager.CurrentCardinalDirection);

        directionManager.UpdateDirection(ActionType.TurnRight);
        Assert.Equal(CardinalDirection.South, directionManager.CurrentCardinalDirection);
    }
}
