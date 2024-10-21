using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public class Car : ICar
{
    private readonly IDirectionManager _directionManager;
    private readonly int _minTankGasLevel = 0;
    private readonly ICarWarningManager _carWarningManager;

    public CardinalDirection CurrentCardinalDirection { get => _directionManager.CurrentCardinalDirection; }
    public DrivingDirection CurrentDrivingDirection { get => _directionManager.CurrentDrivingDirection; }
    public int CurrentGasLevel { get; private set; }
    public int TankCapacity {  get; private set; }
    public WarningState CurrentWarningState { get => _carWarningManager.GetWarningState(CurrentGasLevel); }
    public int NonCriticalGasWarningLevel { get => _carWarningManager.NonCriticalWarningLevel; }
    public int CriticalGasWarningLevel { get => _carWarningManager.CriticalWarningLevel; }

    public Car(IDirectionManager directionManager, int tankCapacity, ICarWarningManager carWarningManager)
    {
        if (tankCapacity < _minTankGasLevel)
            throw new ArgumentOutOfRangeException($"Tank capacity cannot be less than {_minTankGasLevel}");

        _directionManager = directionManager;
        CurrentGasLevel = tankCapacity;         // Default to full tank
        TankCapacity = tankCapacity;
        _carWarningManager = carWarningManager;
    }

    private void UpdateCurrentGasLevel(int gasLevel)
    {
        if (gasLevel > TankCapacity)
            gasLevel = TankCapacity;

        if (gasLevel < _minTankGasLevel)
            gasLevel = _minTankGasLevel;

        CurrentGasLevel = gasLevel;
    }

    private void UpdateCurrentGasLevel(CarGasTankImpact tankImpact)
    {
        switch (tankImpact) 
        {
            case CarGasTankImpact.Decrement:
                UpdateCurrentGasLevel(CurrentGasLevel - 1);
                break;
            case CarGasTankImpact.Fill:
                UpdateCurrentGasLevel(TankCapacity);
                break;
            case CarGasTankImpact.None:
            default:
                break;
        }
    }

    public IActionResult PerformAction(IAction action)
    {
        if (!CanPerformAction(action).IsSuccess)
            return ActionResult.Failure("Action cannot be performed due to current car state.");

        _directionManager.UpdateDirection(action.Type);
        UpdateCurrentGasLevel(action.TankImpact);

        return ActionResult.Success();
    }

    public IActionResult CanPerformAction(IAction action)
    {
        switch (action.Type)
        {
            case ActionType.TurnLeft:
            case ActionType.TurnRight:
            case ActionType.DriveForward:
            case ActionType.DriveReverse:
                if (CurrentGasLevel <= _minTankGasLevel)
                    return ActionResult.Failure("Not enough gas to perform this action.");
                break;
            case ActionType.RefillPetrol:
                if (CurrentGasLevel >= TankCapacity)
                    return ActionResult.Failure("Tank is already full.");
                break;
        }
        
        return ActionResult.Success("Action can be performed.");
    }
}
