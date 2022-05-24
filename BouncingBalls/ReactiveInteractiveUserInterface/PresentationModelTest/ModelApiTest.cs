using NUnit.Framework;
using PresentationModel;

namespace PresentationModelTest;

public class Tests
{
    [Test]
    public void GenerateTest()
    {
        var modelAbstractApi = ModelAbstractApi.CreateApi();
        const int ballsNumber = 10;
        Assert.AreEqual(0, modelAbstractApi.ActualBallsNumber);
        modelAbstractApi.GenerateHandler(ballsNumber);
        Assert.AreEqual(ballsNumber, modelAbstractApi.ActualBallsNumber);
        
    }

    [Test]
    public void StopTest()
    {
        var modelAbstractApi = ModelAbstractApi.CreateApi();
        const int ballsNumber = 10;
        Assert.AreEqual(0, modelAbstractApi.ActualBallsNumber);
        modelAbstractApi.GenerateHandler(ballsNumber);
        Assert.AreEqual(ballsNumber, modelAbstractApi.ActualBallsNumber);
        modelAbstractApi.Stop();
        Assert.AreEqual(0, modelAbstractApi.ActualBallsNumber);
    }
}