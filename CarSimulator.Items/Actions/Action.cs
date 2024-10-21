using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Actions;

public abstract class Action : IAction
{
    public ActionType Type { get; set; }

    public DriverFatigueImpact FatigueImpact { get; set; }
    public CarGasTankImpact TankImpact { get; set; }
}
