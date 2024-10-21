using CarSimulator.Items.Enums;

namespace CarSimulator.Items;

public interface IAction
{
    ActionType Type { get; }
    DriverFatigueImpact FatigueImpact { get; }
    CarGasTankImpact TankImpact { get; }
}
