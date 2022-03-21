using NUnit.Framework;

namespace BasicTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AddTest()
    {
        Assert.AreEqual(Calculator.Add(3.2, 1.6), 4.8);
    }
}