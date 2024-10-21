using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public interface IDriver
{
    int CurrentFatigueLevel { get; }
    int MaxFatigueLevel { get; }
    WarningState CurrentWarningState { get; }
    IActionResult PerformAction(IAction action);
    IActionResult CanPerformAction(IAction action);
}
