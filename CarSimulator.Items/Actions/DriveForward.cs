using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Actions;

public class DriveForward : Action
{
    public DriveForward()
    {
        Type = ActionType.DriveForward;
        FatigueImpact = DriverFatigueImpact.Increment;
        TankImpact = CarGasTankImpact.Decrement;
    }
}
