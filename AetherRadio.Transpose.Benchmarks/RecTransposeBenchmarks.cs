using AetherRadio.Numerics;

using BenchmarkDotNet.Attributes;

namespace AetherRadio.Transpose.Benchmarks;
public class RecTransposeBenchmarks
{
    private readonly int[,] _input;
    private int[,] _output;

    public RecTransposeBenchmarks()
    {
        const int len = 1024;

        _input = new int[len, len];
        _output = new int[len, len];

        for (var i = 0; i < len; i++)
        {
            for (var j = 0; j < len; j++)
            {
                _input[i, j] = i + j;
            }
        }
    }

    [Benchmark]
    public void RecTranspose()
    {
        Functions.RecTranspose<int>(_input, _output);
    }
}

