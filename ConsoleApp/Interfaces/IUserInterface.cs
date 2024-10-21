using CarSimulator.Items.Enums;

namespace CarSimulator.ConsoleApp.Interfaces;

public interface IUserInterface
{
    string ReadInput();
    void WriteOutput(string message);
    void WriteColoredOutput(WarningState warningState, string message);
}
