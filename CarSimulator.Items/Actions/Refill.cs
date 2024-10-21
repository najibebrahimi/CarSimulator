using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Actions;

public class Refill : Action
{
    public Refill()
    {
        Type = ActionType.RefillPetrol;
        FatigueImpact = DriverFatigueImpact.Increment;
        TankImpact = CarGasTankImpact.Fill;
    }
}
