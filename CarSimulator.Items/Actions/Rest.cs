using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Actions;

public class Rest : Action
{
    public Rest()
    {
        Type = ActionType.Rest;
        FatigueImpact = DriverFatigueImpact.Decrement;
        TankImpact = CarGasTankImpact.None;
    }
}
