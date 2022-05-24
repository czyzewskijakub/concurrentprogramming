using Data;
using NUnit.Framework;

namespace DataTest;

public class BallsOperationsTest
{
    [Test]
    public void GeneralTest()
    {
        var ballOperations = new BallOperations();
        const int ballsNumber = 10;
        ballOperations.CreateBalls(ballsNumber);
        Assert.AreEqual(ballsNumber, ballOperations.Balls.Count);
        foreach (var ball in ballOperations.Balls)
        {
            Assert.IsTrue(ball.X > 0 && ball.X < BallOperations.Width && ball.Y > 0 && ball.Y < BallOperations.Height);
        }
        ballOperations.StopBalls();
        Assert.AreEqual(0, ballOperations.Balls.Count);
    }
}