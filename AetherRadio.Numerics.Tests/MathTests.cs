namespace AetherRadio.Numerics.Tests;

[TestFixture]
public class MathTests
{
    [Test]
    public void IsPowerOfTwo_ShouldReturnTrue_ForPowersOfTwo()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Math.IsPowerOfTwo(1));
            Assert.That(Math.IsPowerOfTwo(2));
            Assert.That(Math.IsPowerOfTwo(4));
            Assert.That(Math.IsPowerOfTwo(8));
            Assert.That(Math.IsPowerOfTwo(16));
            Assert.That(Math.IsPowerOfTwo(32));
            Assert.That(Math.IsPowerOfTwo(64));
            Assert.That(Math.IsPowerOfTwo(128));
            Assert.That(Math.IsPowerOfTwo(256));
            Assert.That(Math.IsPowerOfTwo(512));
            Assert.That(Math.IsPowerOfTwo(1024));
        });
    }

    [Test]
    public void IsPowerOfTwo_ShouldReturnFalse_ForNonPowersOfTwo()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Math.IsPowerOfTwo(0), Is.False);
            Assert.That(Math.IsPowerOfTwo(3), Is.False);
            Assert.That(Math.IsPowerOfTwo(5), Is.False);
            Assert.That(Math.IsPowerOfTwo(6), Is.False);
            Assert.That(Math.IsPowerOfTwo(7), Is.False);
            Assert.That(Math.IsPowerOfTwo(9), Is.False);
            Assert.That(Math.IsPowerOfTwo(10), Is.False);
            Assert.That(Math.IsPowerOfTwo(11), Is.False);
            Assert.That(Math.IsPowerOfTwo(12), Is.False);
            Assert.That(Math.IsPowerOfTwo(13), Is.False);
            Assert.That(Math.IsPowerOfTwo(15), Is.False);
            Assert.That(Math.IsPowerOfTwo(17), Is.False);
            Assert.That(Math.IsPowerOfTwo(31), Is.False);
            Assert.That(Math.IsPowerOfTwo(100), Is.False);
            Assert.That(Math.IsPowerOfTwo(1000), Is.False);
        });
    }
}
