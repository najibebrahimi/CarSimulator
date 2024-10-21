using CarSimulator.ConsoleApp.Interfaces;
using CarSimulator.Items.Enums;

namespace CarSimulator.ConsoleApp;

public class ConsoleUserInterface : IUserInterface
{
    public string ReadInput()
    {
        return Console.ReadLine();
    }

    public void WriteColoredOutput(WarningState warningState, string message)
    {
        switch (warningState)
        {
            case WarningState.Critical:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case WarningState.NonCritical:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case WarningState.None:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
        }

        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void WriteOutput(string message)
    {
        Console.WriteLine(message);
    }
}
