using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Interfaces;

public interface IDirectionManager
{
    CardinalDirection CurrentCardinalDirection { get; }
    DrivingDirection CurrentDrivingDirection { get; }

    void UpdateDirection(ActionType action);
}
