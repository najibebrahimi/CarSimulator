﻿using CarSimulator.Items.Enums;
using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items.Warnings;

public class CarWarningManager : BaseWarningManager<int>, ICarWarningManager
{
    //public int NonCriticalWarningLevel { get; private set; }
    //public int CriticalWarningLevel { get; private set; }
    //public WarningCriticalityOrder WarningCriticalityOrder { get; private set; } = WarningCriticalityOrder.Descending;  // Hard-coded at this point, should implement an abstract warning class.

    public CarWarningManager(int nonCriticalWarningLevel, int criticalWarningLevel) : base(nonCriticalWarningLevel, criticalWarningLevel, WarningCriticalityOrder.Descending)
    {
        /*if (nonCriticalWarningLevel <= criticalWarningLevel)
            throw new ArgumentException("Non-critical warning level cannot be less than or equal to critical warning level!");

        NonCriticalWarningLevel = nonCriticalWarningLevel;
        CriticalWarningLevel = criticalWarningLevel;*/
    }

    /*public WarningState GetWarningState(int currentLevel)
    {
        if (currentLevel <= CriticalWarningLevel)
            return WarningState.Critical;

        if (currentLevel <= NonCriticalWarningLevel)
            return WarningState.NonCritical;

        return WarningState.None;
    }*/
}