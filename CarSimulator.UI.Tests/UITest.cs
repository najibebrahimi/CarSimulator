using Moq;

using CarSimulator.ConsoleApp.Interfaces;
using CarSimulator.Items;

public class ProgramTests
{
    [Fact]
    public void HandleUserAction_TurnLeft_PerformsAction()
    {
        var mockUI = new Mock<IUserInterface>();
        var mockSimulator = new Mock<ISimulator>();

        mockSimulator.Setup(s => s.CanPerformAction(It.IsAny<IAction>())).Returns(ActionResult.Success());

        var result = Program.HandleUserAction("1", mockUI.Object, mockSimulator.Object);

        mockSimulator.Verify(s => s.PerformAction(It.IsAny<IAction>()), Times.Once);
        Assert.True(result); 
    }

    [Fact]
    public void HandleUserAction_InvalidInput_ShowsErrorMessage()
    {
        var mockUI = new Mock<IUserInterface>();
        var mockSimulator = new Mock<ISimulator>();

        string outputMessage = null;
        mockUI.Setup(ui => ui.WriteOutput(It.IsAny<string>()))
              .Callback<string>(msg => outputMessage = msg);

        var result = Program.HandleUserAction("invalid", mockUI.Object, mockSimulator.Object);

        Assert.Equal("Invalid selection, please try again.", outputMessage);
        Assert.True(result);
    }

    [Fact]
    public void HandleUserAction_Quit_ReturnsFalse()
    {
        var mockUI = new Mock<IUserInterface>();
        var mockSimulator = new Mock<ISimulator>();

        var result = Program.HandleUserAction("7", mockUI.Object, mockSimulator.Object);

        Assert.False(result);
    }
}
