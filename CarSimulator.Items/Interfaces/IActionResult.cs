namespace CarSimulator.Items.Interfaces;

public interface IActionResult
{
    bool IsSuccess { get; }
    string Message { get; }
}
