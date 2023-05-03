namespace AetherRadio.Numerics.Tests;

[TestFixture]
public class IsPowerOfTwoTests
{
    [Test]
    public void IsPowerOfTwoShouldReturnTrueForPowersOfTwo([Range(0, 16)] int x)
    {
        Assert.That(Functions.IsPowerOfTwo((int)Math.Pow(2, x)));
    }

    [Test]
    public void IsPowerOfTwoShouldReturnFalseForNonPowersOfTwo([Values(0, 3, 5, 6, 7, 9, 10, 11, 12, 13, 15, 17, 31, 100, 1000)] int x)
    {
        Assert.That(Functions.IsPowerOfTwo(x), Is.False);
    }
}
