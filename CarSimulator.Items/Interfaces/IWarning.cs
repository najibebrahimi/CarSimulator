using CarSimulator.Items.Enums;

namespace CarSimulator.Items;

public interface IWarning<T>
{
    T NonCriticalWarningLevel { get; }
    T CriticalWarningLevel { get; }
    WarningCriticalityOrder WarningCriticalityOrder { get; }
    WarningState GetWarningState(T currentLevel);
}
