using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public interface ISimulator
{
    int CurrentDriverFatigueLevel { get; }
    int CurrentCarGasLevel { get; }
    int CarTankCapacity { get; }
    int DriverMaxFatigueLevel { get; }
    WarningState CurrentDriverWarningState { get; }
    WarningState CurrentCarWarningState { get; }
    CardinalDirection CurrentCardinalDirection { get; }
    DrivingDirection CurrentDrivingDirection { get; }
    IActionResult PerformAction(IAction action);
    IActionResult CanPerformAction(IAction action);
}
