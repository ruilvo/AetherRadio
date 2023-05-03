using AetherRadio.Numerics;

namespace AetherRadio.Fft.Tests;

[TestFixture]
public class FftFloatRadix2DifTests
{
    // TODO(ruilvo): Check below.
    //  - Test with FFTs of larger sizes.
    //  - Use the `SequentialAttribute` from NUnit to eliminate the boilerplate code.
    //    - https://docs.nunit.org/articles/nunit/writing-tests/attributes/sequential.html
    //    - Write the test values and the results in ValueAttribute's.

    [Test]
    public void TestMostBasicFft()
    {
        const int size = 2;

        var fft = new FftFloatRadix2Dif(size);

        var input = new ComplexFloat[]
        {
            new(1.0F, 0.0F),
            new(0.0F, 0.0F)
        };

        var output = new ComplexFloat[size];

        var result = new ComplexFloat[]
        {
            new(1.0F, 0.0F),
            new(1.0F, 0.0F)
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
        const int size = 2;

        var fft = new FftFloatRadix2Dif(size);

        var input = new ComplexFloat[]
        {
            new(1.0F, 0.0F),
            new(1.0F, 0.0F)
        };

        var output = new ComplexFloat[size];

        var result = new ComplexFloat[]
        {
            new(2.0F, 0.0F),
            new(0.0F, 0.0F)
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
        const int size = 2;

        var fft = new FftFloatRadix2Dif(size);

        var input = new ComplexFloat[]
        {
            new(1.0F, 1.0F),
            new(1.0F, 0.0F)
        };

        var output = new ComplexFloat[size];

        var result = new ComplexFloat[]
        {
            new(2.0F, 1.0F),
            new(0.0F, 1.0F)
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
