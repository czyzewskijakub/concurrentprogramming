using System;
using System.Linq;
using NUnit.Framework;

namespace PresentationViewModelTest;

public class MainWindowViewModelUnitTest
{
    [Test]
    public void ConstructorTest()
    {
        var vm = new MainWindowViewModel
        {
            Radius = 2,
            CanvasHeight = 50, // Should not change
            CanvasWidth = 75,  // Should not change
            BallsNumber = 10
        };

        // Fields check
        Assert.AreEqual(2, vm.Radius);
        Assert.AreEqual(300, vm.CanvasHeight);
        Assert.AreEqual(600,vm.CanvasWidth);
        Assert.AreEqual(10, vm.BallsNumber);
    }

    [Test]
    public void CommandsTest()
    {
        var vm = new MainWindowViewModel();
        // Commands callings
        Assert.NotNull(vm.GenerateCommand);
        Assert.IsTrue(vm.GenerateCommand.CanExecute(null));
        Assert.NotNull(vm.StartMoving);
        Assert.IsTrue(vm.StartMoving.CanExecute(null));
        Assert.NotNull(vm.StopMoving);
        Assert.IsTrue(vm.StopMoving.CanExecute(null));
        Assert.NotNull(vm.ClearBoard);
        Assert.IsTrue(vm.ClearBoard.CanExecute(null));
    }

    [Test]
    public void GenerateTest()
    {
        var vm = new MainWindowViewModel()
        {
            BallsNumber = 100
        };
        
        Assert.AreEqual(0, vm.Coordinates.Count);
        vm.GenerateCommand.Execute(null);
        Assert.AreEqual(100, vm.Coordinates.Count);
    }

    [Test]
    public void CoordinatesRangeTest()
    { 
        var vm = new MainWindowViewModel() {BallsNumber = 10};
        vm.GenerateCommand.Execute(null);
        Assert.IsTrue(vm.Coordinates.All(p => p.X - vm.Radius >= 0 && p.X + vm.Radius <= vm.CanvasWidth && p.Y - vm.Radius >= 0 && p.Y + vm.Radius <= vm.CanvasHeight));
    }

    [Test]
    public void MoveBallTest()
    {
        var vm = new MainWindowViewModel() {BallsNumber = 10};
        vm.GenerateCommand.Execute(null);
        var coordinatesCopy = vm.Coordinates;
        vm.StartMoving.Execute(null);
        Assert.IsTrue(!vm.Coordinates.Where((t, i) => Math.Abs(t.X - coordinatesCopy[i].X) > 1 || Math.Abs(t.Y - coordinatesCopy[i].Y) > 1).Any());
    }

    [Test]
    public void ClearBoardTest()
    {
        var vm = new MainWindowViewModel() {BallsNumber = 10};
        vm.GenerateCommand.Execute(null);
        Assert.AreEqual(10, vm.Coordinates.Count);
        vm.ClearBoard.Execute(null);
        Assert.AreEqual(0, vm.Coordinates.Count);
    }
}