using BenchmarkDotNet.Running;

namespace AetherRadio.Transpose.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<RecTransposeBenchmarks>();
    }
}