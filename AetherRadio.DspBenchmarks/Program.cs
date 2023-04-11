using BenchmarkDotNet.Running;

namespace AetherRadio.DspBenchmarks;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<FftBenchmarks>();
    }
}