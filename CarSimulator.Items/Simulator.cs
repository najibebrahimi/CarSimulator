using CarSimulator.Items;
using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public class Simulator : ISimulator
{
    private readonly ICar _car;
    private readonly IDriver _driver;

    public Simulator(ICar car, IDriver driver)
    {
        _car = car; 
        _driver = driver;
    }

    public int CurrentDriverFatigueLevel => _driver.CurrentFatigueLevel;
    public int CurrentCarGasLevel => _car.CurrentGasLevel;
    public int CarTankCapacity => _car.TankCapacity;
    public int DriverMaxFatigueLevel => _driver.MaxFatigueLevel;
    public WarningState CurrentDriverWarningState => _driver.CurrentWarningState;
    public CardinalDirection CurrentCardinalDirection => _car.CurrentCardinalDirection;
    public DrivingDirection CurrentDrivingDirection => _car.CurrentDrivingDirection;
    public WarningState CurrentCarWarningState => _car.CurrentWarningState;

    public IActionResult CanPerformAction(IAction action)
    {
        var canPerformCarActionResult = _car.CanPerformAction(action);
        var canPerformDriverActionResult = _driver.CanPerformAction(action);

        if (canPerformCarActionResult.IsSuccess && canPerformDriverActionResult.IsSuccess)
            return ActionResult.Success("Action can be performed by both car and driver.");
        if (!canPerformCarActionResult.IsSuccess && canPerformDriverActionResult.IsSuccess)
            return ActionResult.Failure("Action cannot be performed by the car.");
        if (canPerformCarActionResult.IsSuccess && !canPerformDriverActionResult.IsSuccess)
            return ActionResult.Failure("Action cannot be performed by the driver.");
        
        return ActionResult.Failure("Action cannot be performed by both car and driver.");
    }

    public IActionResult PerformAction(IAction action)
    {
        var canPerformActionResult = CanPerformAction(action);
        if (!canPerformActionResult.IsSuccess)
            return canPerformActionResult;

        var carActionResult = _car.PerformAction(action);
        var driverActionResult = _driver.PerformAction(action);

        if (carActionResult.IsSuccess && driverActionResult.IsSuccess)
            return ActionResult.Success("Action can be performed by both car and driver.");
        if (!carActionResult.IsSuccess && driverActionResult.IsSuccess)
            return ActionResult.Failure("Action cannot be performed by the car.");
        if (carActionResult.IsSuccess && !driverActionResult.IsSuccess)
            return ActionResult.Failure("Action cannot be performed by the driver.");

        return ActionResult.Failure("Action cannot be performed by both car and driver.");
    }
}
