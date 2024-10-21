using CarSimulator.Items.Enums;

namespace CarSimulator.Items.Warnings;

public abstract class BaseWarningManager<T> : IWarning<T> where T : IComparable<T>
{
    public T NonCriticalWarningLevel { get; protected set; }

    public T CriticalWarningLevel {  get; protected set; }

    public WarningCriticalityOrder WarningCriticalityOrder {  get; protected set; }

    protected BaseWarningManager(T nonCriticalWarningLevel, T criticalWarningLevel, WarningCriticalityOrder warningCriticalityOrder)
    {
        if (warningCriticalityOrder == WarningCriticalityOrder.Ascending && nonCriticalWarningLevel.CompareTo(criticalWarningLevel) >= 0)
            throw new ArgumentException("Non-critical warning level cannot be greater than or equal to the critical warning level in ascending order.");

        if (warningCriticalityOrder == WarningCriticalityOrder.Descending && nonCriticalWarningLevel.CompareTo(criticalWarningLevel) <= 0)
            throw new ArgumentException("Non-critical warning level cannot be less than or equal to the critical warning level in descending order.");

        NonCriticalWarningLevel = nonCriticalWarningLevel;
        CriticalWarningLevel = criticalWarningLevel;
        WarningCriticalityOrder = warningCriticalityOrder;
    }

    public WarningState GetWarningState(T currentLevel)
    {
        if (WarningCriticalityOrder == WarningCriticalityOrder.Ascending)
        {
            if (currentLevel.CompareTo(CriticalWarningLevel) >= 0)
                return WarningState.Critical;

            if (currentLevel.CompareTo(NonCriticalWarningLevel) >= 0)
                return WarningState.NonCritical;
        }
        else // Descending
        {
            if (currentLevel.CompareTo(CriticalWarningLevel) <= 0)
                return WarningState.Critical;

            if (currentLevel.CompareTo(NonCriticalWarningLevel) <= 0)
                return WarningState.NonCritical;
        }

        return WarningState.None;
    }
}
