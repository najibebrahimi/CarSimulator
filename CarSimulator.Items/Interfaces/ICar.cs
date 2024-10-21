using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public interface ICar
{
    CardinalDirection CurrentCardinalDirection { get; }
    DrivingDirection CurrentDrivingDirection { get; }
    WarningState CurrentWarningState { get; }
    int CurrentGasLevel { get; }
    int TankCapacity { get; }
    IActionResult PerformAction(IAction action);
    IActionResult CanPerformAction(IAction action);

}
