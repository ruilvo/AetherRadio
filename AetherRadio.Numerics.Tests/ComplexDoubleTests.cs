namespace AetherRadio.Numerics.Tests;

[TestFixture]
public class ComplexDoubleTests
{
    [Test]
    public void TestMagnitude()
    {
        var c = new ComplexDouble(3, 4);
        Assert.That(c.Magnitude, Is.EqualTo(5));
    }

    [Test]
    public void TestPhase()
    {
        var c = new ComplexDouble(1, 1);
        Assert.That(c.Phase, Is.EqualTo(MathF.PI / 4).Within(1e-6));
    }

    [Test]
    public void TestNormalized()
    {
        var c = new ComplexDouble(3, 4);
        var norm = c.Normalized;
        Assert.Multiple(() =>
        {
            Assert.That(norm.Real, Is.EqualTo(0.6).Within(1e-6));
            Assert.That(norm.Imaginary, Is.EqualTo(0.8).Within(1e-6));
        });
    }

    [Test]
    public void TestFromPolar()
    {
        var c = ComplexDouble.FromPolar(5, MathF.PI / 3);
        Assert.Multiple(() =>
        {
            Assert.That(c.Real, Is.EqualTo(2.5).Within(1e-6));
            Assert.That(c.Imaginary, Is.EqualTo(4.330127).Within(1e-6));
        });
    }

    [Test]
    public void TestAddition()
    {
        var c1 = new ComplexDouble(3, 4);
        var c2 = new ComplexDouble(1, 2);
        var sum = c1 + c2;
        Assert.Multiple(() =>
        {
            Assert.That(sum.Real, Is.EqualTo(4));
            Assert.That(sum.Imaginary, Is.EqualTo(6));
        });
    }

    [Test]
    public void TestSubtraction()
    {
        var c1 = new ComplexDouble(3, 4);
        var c2 = new ComplexDouble(1, 2);
        var diff = c1 - c2;
        Assert.Multiple(() =>
        {
            Assert.That(diff.Real, Is.EqualTo(2));
            Assert.That(diff.Imaginary, Is.EqualTo(2));
        });
    }

    [Test]
    public void TestMultiplication()
    {
        var c1 = new ComplexDouble(3, 4);
        var c2 = new ComplexDouble(1, 2);
        var product = c1 * c2;
        Assert.Multiple(() =>
        {
            Assert.That(product.Real, Is.EqualTo(-5).Within(1e-6));
            Assert.That(product.Imaginary, Is.EqualTo(10).Within(1e-6));
        });
    }

    [Test]
    public void TestDivision()
    {
        var c1 = new ComplexDouble(3, 4);
        var c2 = new ComplexDouble(1, 2);
        var quotient = c1 / c2;
        Assert.Multiple(() =>
        {
            Assert.That(quotient.Real, Is.EqualTo(2).Within(1e-6));
            Assert.That(quotient.Imaginary, Is.EqualTo(0).Within(1e-6));
        });
    }

    [Test]
    public void TestNormalization()
    {
        var c = new ComplexDouble(3, 4);
        c.Normalize();
        Assert.Multiple(() =>
        {
            Assert.That(c.Real, Is.EqualTo(0.6).Within(1e-6));
            Assert.That(c.Imaginary, Is.EqualTo(0.8).Within(1e-6));
        });
    }
}
