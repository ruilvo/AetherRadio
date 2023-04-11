using AetherRadio.Dsp;
using BenchmarkDotNet.Attributes;

namespace AetherRadio.DspBenchmarks;

public class FftBenchmarks
{
    private const int _sampleCount = 1024;
    private readonly float[] _samples;
    private readonly Fft _fft;

    public FftBenchmarks()
    {
        _samples = new float[_sampleCount];
        _fft = new Fft(_sampleCount);

        _samples[0] = 1.0F;
    }

    [Benchmark]
    public void Fft()
    {
        _fft.ExecuteInPlace(_samples);
    }

}