using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items.Warnings;

public class DriverWarningManager : BaseWarningManager<int>, IDriverWarningManager
{
    //public int NonCriticalWarningLevel { get; private set; }
    //public int CriticalWarningLevel { get; private set; }
    //public WarningCriticalityOrder WarningCriticalityOrder { get; private set; } = WarningCriticalityOrder.Ascending;   // Hard-coded at this point, should implement an abstract warning class.

    public DriverWarningManager(int nonCriticalWarningLevel, int criticalWarningLevel) : base(nonCriticalWarningLevel, criticalWarningLevel, WarningCriticalityOrder.Ascending)
    {
        /*if (nonCriticalWarningLevel >= criticalWarningLevel)
            throw new ArgumentException("Non-critical warning level cannot be greater than or equal to critical warning level!");

        NonCriticalWarningLevel = nonCriticalWarningLevel;
        CriticalWarningLevel = criticalWarningLevel;*/
    }

    /*public WarningState GetWarningState(int currentLevel)
    {
        if (currentLevel >= CriticalWarningLevel)
            return WarningState.Critical;

        if (currentLevel >= NonCriticalWarningLevel)
            return WarningState.NonCritical;

        return WarningState.None;
    }*/
}