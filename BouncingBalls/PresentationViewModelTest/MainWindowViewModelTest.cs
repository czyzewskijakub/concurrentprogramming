using BouncingBalls.ViewModels;
using Moq;
using NUnit.Framework;

namespace PresentationViewModelTest
{
    [TestFixture]
    public class MainWindowViewModelTest
    {
        [Test]
        public void ConstructorTest()
        {
            var window = new MainWindowViewModel();

            Assert.IsNotNull(window.GenerateCommand);
            Assert.IsTrue(window.GenerateCommand.CanExecute(null));
            Assert.IsNotNull(window.StartMoving);
            Assert.IsTrue(window.StartMoving.CanExecute(null));
            Assert.IsNotNull(window.StopMoving);
            Assert.IsTrue(window.StopMoving.CanExecute(null));
            Assert.IsNotNull(window.ClearBoard);
            Assert.IsTrue(window.ClearBoard.CanExecute(null));
            
            
        }
    }
}