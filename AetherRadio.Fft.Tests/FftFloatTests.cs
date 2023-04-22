using AetherRadio.Numerics;

namespace AetherRadio.Fft.Tests;

[TestFixture]
public class FftFloatTests
{
    [Test]
    public void TestMostBasicFft()
    {
        const uint size = 2;

        var fft = new FftFloat(size);

        var input = new ComplexFloat[]
        {
            new ComplexFloat(1.0F, 0.0F), 
            new ComplexFloat(0.0F, 0.0F)
        };

        var output = new ComplexFloat[size];

        var result = new ComplexFloat[]
        {
            new ComplexFloat(1.0F, 0.0F),
            new ComplexFloat(0.0F, 0.0F)
        };

        fft.Transform(input, output);
        Assert.Multiple(() =>
        {
            for(var i = 0; i < size; ++i)
            {
                Assert.That(output[i].Real, Is.EqualTo(result[i].Real).Within(1e-6));
                Assert.That(output[i].Imaginary, Is.EqualTo(result[i].Imaginary).Within(1e-6));
            }
        });
    }

    [Test]
    public void TestReallySimpleFft()
    {
        const uint size = 2;

        var fft = new FftFloat(size);

        var input = new ComplexFloat[]
        {
            new ComplexFloat(1.0F, 0.0F),
            new ComplexFloat(1.0F, 0.0F)
        };

        var output = new ComplexFloat[size];

        var result = new ComplexFloat[]
        {
            new ComplexFloat(2.0F, 0.0F),
            new ComplexFloat(0.0F, 0.0F)
        };

        fft.Transform(input, output);
        Assert.Multiple(() =>
        {
            for (var i = 0; i < size; ++i)
            {
                Assert.That(output[i].Real, Is.EqualTo(result[i].Real).Within(1e-6));
                Assert.That(output[i].Imaginary, Is.EqualTo(result[i].Imaginary).Within(1e-6));
            }
        });
    }

    [Test]
    public void TestSimpleFftWithImaginaryPart()
    {
        const uint size = 2;

        var fft = new FftFloat(size);

        var input = new ComplexFloat[]
        {
            new ComplexFloat(1.0F, 1.0F),
            new ComplexFloat(1.0F, 0.0F)
        };

        var output = new ComplexFloat[size];

        var result = new ComplexFloat[]
        {
            new ComplexFloat(2.0F, 1.0F),
            new ComplexFloat(0.0F, 1.0F)
        };

        fft.Transform(input, output);
        Assert.Multiple(() =>
        {
            for (var i = 0; i < size; ++i)
            {
                Assert.That(output[i].Real, Is.EqualTo(result[i].Real).Within(1e-6));
                Assert.That(output[i].Imaginary, Is.EqualTo(result[i].Imaginary).Within(1e-6));
            }
        });
    }
}
