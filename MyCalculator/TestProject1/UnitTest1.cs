using Calculator;
using NUnit.Framework;

namespace TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AddTest()
    {
        Assert.That(Operations.Add(3.2, 1.6), Is.EqualTo(4.8).Within(0.001));
    }

    [Test]
    public void SubTest()
    {
        Assert.That(Operations.Sub(5.3, 10.1), Is.EqualTo(-4.8).Within(0.001));
    }

    [Test]
    public void MulTest()
    {
        Assert.That(Operations.Mul(1.2, 3), Is.EqualTo(3.6).Within(0.001));
    }

    [Test]
    public void DivTest()
    {
        Assert.That(Operations.Div(6.2, 2.0), Is.EqualTo(3.1).Within(0.000001));
    }
}