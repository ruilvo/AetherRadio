using AetherRadio.Numerics;

using BenchmarkDotNet.Attributes;

namespace AetherRadio.Fft.Benchmarks;

public class FftFloatRadix2DifBenchmarks
{
    private readonly FftFloatRadix2Dif _fft;
    private readonly ComplexFloat[] _input;
    private ComplexFloat[] _output;

    public FftFloatRadix2DifBenchmarks()
    {
        _fft = new FftFloatRadix2Dif(1024);
        _input = new ComplexFloat[1024];

        for (var i = 0; i < _input.Length; ++i)
        {
            // Generate some "random" input.
            _input[i] = new ComplexFloat(i % 2 == 0 ? 1.1F : 2.8F, i % 2 == 0 ? 0.1F : 0.3F);
        }

        _output = new ComplexFloat[1024];
    }

    [Benchmark]
    public void Transform()
    {
        _fft.Transform(_input, _output);
    }
    
}

