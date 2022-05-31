using Data;
using Logic;
using NUnit.Framework;

namespace LogicTest;

public class Tests
{
    [Test]
    public void GenerateTest()
    {
        var logicApi = LogicAbstractApi.CreateApi(new DataApi());
        logicApi.GenerateBalls(10);
        Assert.AreEqual(10, logicApi.GetBalls().Count);
    }
}