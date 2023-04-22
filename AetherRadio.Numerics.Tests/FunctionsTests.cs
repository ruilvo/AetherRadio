namespace AetherRadio.Numerics.Tests;

[TestFixture]
public class FunctionsTests
{
    [Test]
    public void IsPowerOfTwo_ShouldReturnTrue_ForPowersOfTwo()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Functions.IsPowerOfTwo(1));
            Assert.That(Functions.IsPowerOfTwo(2));
            Assert.That(Functions.IsPowerOfTwo(4));
            Assert.That(Functions.IsPowerOfTwo(8));
            Assert.That(Functions.IsPowerOfTwo(16));
            Assert.That(Functions.IsPowerOfTwo(32));
            Assert.That(Functions.IsPowerOfTwo(64));
            Assert.That(Functions.IsPowerOfTwo(128));
            Assert.That(Functions.IsPowerOfTwo(256));
            Assert.That(Functions.IsPowerOfTwo(512));
            Assert.That(Functions.IsPowerOfTwo(1024));
        });
    }

    [Test]
    public void IsPowerOfTwo_ShouldReturnFalse_ForNonPowersOfTwo()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Functions.IsPowerOfTwo(0), Is.False);
            Assert.That(Functions.IsPowerOfTwo(3), Is.False);
            Assert.That(Functions.IsPowerOfTwo(5), Is.False);
            Assert.That(Functions.IsPowerOfTwo(6), Is.False);
            Assert.That(Functions.IsPowerOfTwo(7), Is.False);
            Assert.That(Functions.IsPowerOfTwo(9), Is.False);
            Assert.That(Functions.IsPowerOfTwo(10), Is.False);
            Assert.That(Functions.IsPowerOfTwo(11), Is.False);
            Assert.That(Functions.IsPowerOfTwo(12), Is.False);
            Assert.That(Functions.IsPowerOfTwo(13), Is.False);
            Assert.That(Functions.IsPowerOfTwo(15), Is.False);
            Assert.That(Functions.IsPowerOfTwo(17), Is.False);
            Assert.That(Functions.IsPowerOfTwo(31), Is.False);
            Assert.That(Functions.IsPowerOfTwo(100), Is.False);
            Assert.That(Functions.IsPowerOfTwo(1000), Is.False);
        });
    }
}
