using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public class Driver : IDriver
{
    private int _currentFatigueLevel;
    private readonly int _minFatigueLevel = 0;
    private readonly IDriverWarningManager _driverWarningManager;

    public int CurrentFatigueLevel
    {
        get => _currentFatigueLevel;
        private set
        {
            if (value <= MaxFatigueLevel && value >= _minFatigueLevel)
                _currentFatigueLevel = value;
        }
    }

    public int MaxFatigueLevel { get; private set; }
    public int NonCriticalFatigueWarningLevel { get => _driverWarningManager.NonCriticalWarningLevel; }
    public int CriticalFatigueWarningLevel { get => _driverWarningManager.CriticalWarningLevel; }
    public WarningState CurrentWarningState { get => _driverWarningManager.GetWarningState(CurrentFatigueLevel); }

    public Driver(int maxFatigueLevel, IDriverWarningManager driverWarningManager)
    {
        if (maxFatigueLevel < _minFatigueLevel)
            throw new ArgumentOutOfRangeException($"Max fatigue level cannot be less than {_minFatigueLevel}");

        CurrentFatigueLevel = 0;
        MaxFatigueLevel = maxFatigueLevel;
        _driverWarningManager = driverWarningManager;
    }

    private void UpdateCurrentFatigueLevel(DriverFatigueImpact fatigueImpact)
    {
        switch (fatigueImpact)
        {
            case DriverFatigueImpact.Increment:
                CurrentFatigueLevel++;
                break;
            case DriverFatigueImpact.Decrement:
                CurrentFatigueLevel--;
                break;
            case DriverFatigueImpact.None:
            default:
                break;
        }
    }

    public IActionResult PerformAction(IAction action)
    {
        if (!CanPerformAction(action).IsSuccess)
            return ActionResult.Failure("Action cannot be performed due to current driver state.");

        UpdateCurrentFatigueLevel(action.FatigueImpact);

        return ActionResult.Success();
    }

    public IActionResult CanPerformAction(IAction action)
    {
        return ActionResult.Success();    // Bypassing any warnings
    }
}
