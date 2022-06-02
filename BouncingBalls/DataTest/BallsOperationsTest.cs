using System.IO;
using System.Threading.Tasks;
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

    [Test]
    public void LogTest()
    {
        var ballOperations = new BallOperations();
        const string path = "../../../../log.json";
        const int ballsNumber = 10;
        ballOperations.JsonFilename = path;
        ballOperations.CreateBalls(ballsNumber);
        Task.Delay(5000);
        ballOperations.StopBalls();
        Task.Delay(1000);
        Assert.True(File.Exists(path));
    }
}