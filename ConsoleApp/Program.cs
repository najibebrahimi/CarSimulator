using CarSimulator.ConsoleApp.Interfaces;
using CarSimulator.ConsoleApp;
using CarSimulator.Items.Actions;
using CarSimulator.Items.Enums;
using CarSimulator.Items.Warnings;
using CarSimulator.Items;

public class Program
{
    public static void Main(string[] args)
    {
        // Initialize UI and other dependencies for the console application
        var ui = new ConsoleUserInterface();
        var directionManager = new DirectionManager(CardinalDirection.North);
        var carWarningManager = new CarWarningManager(nonCriticalWarningLevel: 4, criticalWarningLevel: 2);
        var driverWarningManager = new DriverWarningManager(nonCriticalWarningLevel: 6, criticalWarningLevel: 9);
        var driver = new Driver(maxFatigueLevel: 10, driverWarningManager);
        var car = new Car(directionManager, tankCapacity: 20, carWarningManager);
        var simulator = new Simulator(car, driver);

        // Pass the dependencies to the simulation run method
        RunSimulation(ui, simulator);
    }

    public static void RunSimulation(IUserInterface ui, ISimulator simulator)
    {
        bool shouldRun = true;

        while (shouldRun)
        {
            PrintMenu(ui);
            string userInput = ui.ReadInput();
            shouldRun = HandleUserAction(userInput, ui, simulator);

            if (shouldRun)
                PrintStatus(ui, simulator);
        }
    }

    public static bool HandleUserAction(string userInput, IUserInterface ui, ISimulator simulator)
    {
        IAction action;

        switch (userInput)
        {
            case "1":
                ui.WriteOutput("You chose to turn left.");
                action = new TurnLeft();
                break;
            case "2":
                ui.WriteOutput("You chose to turn right.");
                action = new TurnRight();
                break;
            case "3":
                ui.WriteOutput("You chose to drive forward.");
                action = new DriveForward();
                break;
            case "4":
                ui.WriteOutput("You chose to reverse.");
                action = new Reverse();
                break;
            case "5":
                ui.WriteOutput("You chose to take a break.");
                action = new Rest();
                break;
            case "6":
                ui.WriteOutput("You chose to refill.");
                action = new Refill();
                break;
            case "7":
                ui.WriteOutput("Exiting the program...");
                return false;
            default:
                ui.WriteOutput("Invalid selection, please try again.");
                return true;
        }

        var actionResult = simulator.CanPerformAction(action);
        if (actionResult.IsSuccess)
        {
            simulator.PerformAction(action);
        }
        else
        {
            ui.WriteOutput($"Action cannot be performed: {actionResult.Message}");
        }

        return true;
    }

    public static void PrintMenu(IUserInterface ui)
    {
        ui.WriteOutput("1. Turn left");
        ui.WriteOutput("2. Turn right");
        ui.WriteOutput("3. Drive forward");
        ui.WriteOutput("4. Reverse");
        ui.WriteOutput("5. Take a break");
        ui.WriteOutput("6. Refill");
        ui.WriteOutput("7. Quit");
        ui.WriteOutput("What do you want to do next?");
    }

    public static void PrintStatus(IUserInterface ui, ISimulator simulator)
    {
        WarningState currentDriverWarningStatus = simulator.CurrentDriverWarningState;
        WarningState currentCarWarningStatus = simulator.CurrentCarWarningState;

        ui.WriteOutput($"Cardinal direction: {simulator.CurrentCardinalDirection}");
        ui.WriteOutput($"Driving direction: {simulator.CurrentDrivingDirection}");
        ui.WriteColoredOutput(currentCarWarningStatus, $"Gas level: {simulator.CurrentCarGasLevel}/{simulator.CarTankCapacity}");
        ui.WriteColoredOutput(currentDriverWarningStatus, $"Driver fatigue level: {simulator.CurrentDriverFatigueLevel}/{simulator.DriverMaxFatigueLevel}");
        ui.WriteOutput(Environment.NewLine);
    }
}
