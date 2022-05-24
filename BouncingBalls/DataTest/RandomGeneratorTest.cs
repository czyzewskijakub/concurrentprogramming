using Data;
using NUnit.Framework;

namespace DataTest;

public class RandomGeneratorTest
{
    [Test]
    public void NextFloatTest()
    {
        var randomGenerator = new RandomGenerator();
        var number = randomGenerator.NextFloat(-2.1, 36.5);
        Assert.IsTrue(number >= -2.1 && number <= 36.5);
    }
}