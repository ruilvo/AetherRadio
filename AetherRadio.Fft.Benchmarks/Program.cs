using BenchmarkDotNet.Running;

namespace AetherRadio.Fft.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    { 
        BenchmarkRunner.Run<FftSingleBenchmarks>();
    }
}