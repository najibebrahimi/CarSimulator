using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Actions;

public class TurnRight : Action
{
    public TurnRight()
    {
        Type = ActionType.TurnRight;
        FatigueImpact = DriverFatigueImpact.Increment;
        TankImpact = CarGasTankImpact.Decrement;
    }
}
