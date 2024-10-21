using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public class DirectionManager : IDirectionManager
{
    public CardinalDirection CurrentCardinalDirection { get; private set; }
    public DrivingDirection CurrentDrivingDirection { get; private set; }

    public DirectionManager(CardinalDirection initialDirection)
    {
        CurrentCardinalDirection = initialDirection;
        CurrentDrivingDirection = DrivingDirection.Forward;     // Default
    }

    public void UpdateDirection(ActionType action)
    {
        switch (action)
        {
            case ActionType.TurnLeft:
                CurrentCardinalDirection = TurnLeft(CurrentCardinalDirection);
                break;
            case ActionType.TurnRight:
                CurrentCardinalDirection = TurnRight(CurrentCardinalDirection);
                break;
            case ActionType.DriveReverse:
                CurrentCardinalDirection = Reverse(CurrentCardinalDirection);
                CurrentDrivingDirection = DrivingDirection.Reverse;
                break;
            case ActionType.DriveForward:
                CurrentCardinalDirection = Forward(CurrentCardinalDirection);
                CurrentDrivingDirection = DrivingDirection.Forward;
                break;
            default:
                break;
        }
    }

    private static CardinalDirection GetOppositeDirection(CardinalDirection direction)
    {
        return direction switch
        {
            CardinalDirection.North => CardinalDirection.South,
            CardinalDirection.South => CardinalDirection.North,
            CardinalDirection.East => CardinalDirection.West,
            CardinalDirection.West => CardinalDirection.East,
            _ => direction
        };
    }

    private CardinalDirection Forward(CardinalDirection currentDirection)
    {
        if (CurrentDrivingDirection == DrivingDirection.Reverse)
            return GetOppositeDirection(currentDirection);  // Switching from reverse to forward

        return currentDirection;
    }

    private CardinalDirection Reverse(CardinalDirection currentDirection)
    {
        if (CurrentDrivingDirection == DrivingDirection.Forward)
            return GetOppositeDirection(currentDirection);  // Switching from forward to reverse

        return currentDirection;       
    }

    private CardinalDirection TurnLeft(CardinalDirection currentDirection)
    {
        return currentDirection switch
        {
            CardinalDirection.North => (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.West) : CardinalDirection.West,
            CardinalDirection.West => (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.South) : CardinalDirection.South,
            CardinalDirection.South => (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.East) : CardinalDirection.East,
            CardinalDirection.East => (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.North) : CardinalDirection.North,
            _ => currentDirection
        };
    }

    private CardinalDirection TurnRight(CardinalDirection currentDirection)
    {
        return currentDirection switch
        {
            CardinalDirection.North => (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.East) : CardinalDirection.East,
            CardinalDirection.East=> (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.South) : CardinalDirection.South,
            CardinalDirection.South => (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.West) : CardinalDirection.West,
            CardinalDirection.West=> (CurrentDrivingDirection == DrivingDirection.Reverse) ? GetOppositeDirection(CardinalDirection.North) : CardinalDirection.North,
            _ => currentDirection
        };
    }
}
