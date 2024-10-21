using CarSimulator.Items.Interfaces;

namespace CarSimulator.Items;

public class ActionResult : IActionResult
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public ActionResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static ActionResult Success(string message = "Action performed successfully.")
    {
        return new ActionResult(true, message);
    }

    public static ActionResult Failure(string message)
    {
        return new ActionResult(false, message);
    }
}
