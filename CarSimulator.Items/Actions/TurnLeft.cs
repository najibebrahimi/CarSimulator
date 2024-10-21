using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Actions;

public class TurnLeft : Action
{
    public TurnLeft()
    {
        Type = ActionType.TurnLeft;
        FatigueImpact = DriverFatigueImpact.Increment;
        TankImpact = CarGasTankImpact.Decrement;
    }
}
