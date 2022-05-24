using System.Numerics;
using Data;
using NUnit.Framework;

namespace DataTest;

public class Tests
{

    [Test]
    public void ConstructorTest()
    {
        var xPos = 5;
        var yPos = 7;
        var radius = 10;
        var mass = 3;
        var velocity = new Vector2(5, 5);
        var ballData = new BallData(xPos, yPos, radius, mass, velocity);
        Assert.AreEqual(xPos, ballData.X);
        Assert.AreEqual(yPos, ballData.Y);
        Assert.AreEqual(radius, ballData.Radius);
        Assert.AreEqual(mass, ballData.Mass);
        Assert.AreEqual(velocity, ballData.Velocity);
    }

    [Test]
    public void UpdateTest()
    {
        var ballData = new BallData(5, 7, 10, 3, new Vector2(5, 5));
        ballData.Update();
        Assert.AreEqual(10, ballData.X);
        Assert.AreEqual(12, ballData.Y);
    }
}