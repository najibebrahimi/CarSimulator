using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Actions;

public class Reverse : Action
{
    public Reverse()
    {
        Type = ActionType.DriveReverse;
        FatigueImpact = DriverFatigueImpact.Increment;
        TankImpact = CarGasTankImpact.Decrement;
    }
}
