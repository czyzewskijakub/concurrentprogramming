using Moq;
using NUnit.Framework;
using PresentationViewModel;

namespace PresentationViewModelTest;

public class MainWindowViewModelUnitTest
{
    [Test]
    public void ConstructorTest()
    {
        var vm = new MainWindowViewModel
        {
            Radius = 2,
            CanvasHeight = 50,
            CanvasWidth = 75,
            BallsNumber = 10
        };

        // Fields check
        Assert.AreEqual(2, vm.Radius);
        Assert.AreEqual(50, vm.CanvasHeight);
        Assert.AreEqual(75,vm.CanvasWidth);
        Assert.AreEqual(10, vm.BallsNumber);
    }

    [Test]
    public void CommandsTest()
    {
        var vm = new MainWindowViewModel();
        // Commands callings
        Assert.NotNull(vm.StartCommand);
        Assert.IsTrue(vm.StartCommand.CanExecute(null));
        Assert.NotNull(vm.StopCommand);
        Assert.IsTrue(vm.StopCommand.CanExecute(null));
    }
    

    [Test]
    public void SimpleMockTest()
    {
        var mock = new Mock<MainWindowViewModel>
        {
            Object =
            {
                BallsNumber = 20
            }
        };
        Assert.AreEqual(mock.Object.BallsNumber, 20);
    }
}