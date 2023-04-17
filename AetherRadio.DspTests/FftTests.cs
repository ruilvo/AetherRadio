using AetherRadio.Dsp;

namespace AetherRadio.DspTests;

[TestFixture]
public class FftTests
{
    private FftDouble _fft;
    private int _size;

    [SetUp]
    public void Setup()
    {
        _size = 2;
        _fft = new FftDouble(_size);
    }

    [Test]
    public void BasicTest()
    {
        var data = new float[] { 1.0F, 0.0F };
        _fft.ExecuteInPlace(data);
        Assert.Multiple(() =>
        {
            Assert.That(data[0], Is.EqualTo(1.0F).Within(0.0001F));
            Assert.That(data[1], Is.EqualTo(1.0F).Within(0.0001F));
        });
    }

    [Test]
    public void TrivialTest()
    {
        var data = new float[] { 1.0F, 1.0F };
        _fft.ExecuteInPlace(data);
        Assert.Multiple(() =>
        {
            Assert.That(data[0], Is.EqualTo(2.0F).Within(0.0001F));
            Assert.That(data[1], Is.EqualTo(0.0F).Within(0.0001F));
        });
    }

    [Test]
    public void NonTrivialTest()
    {
        var data = new float[] { 5.0F, 1.0F };
        _fft.ExecuteInPlace(data);
        Assert.Multiple(() =>
        {
            Assert.That(data[0], Is.EqualTo(6.0F).Within(0.0001F));
            Assert.That(data[1], Is.EqualTo(4.0F).Within(0.0001F));
        });
    }
}