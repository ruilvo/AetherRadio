using System.Diagnostics;

using CommunityToolkit.HighPerformance;

namespace AetherRadio.Numerics;

public static partial class Functions
{
    public static void CacheObliviousTranspose<T>(
        ReadOnlySpan2D<T> input,
        Span2D<T> output)
    {
        Debug.Assert(input.Length == output.Length);
        Debug.Assert(input.Height == output.Width);
        Debug.Assert(input.Width == output.Height);

        // Get the dimensions of the input and output spans
        var rows = input.Height;
        var cols = input.Width;

        // If the input or output spans are too small, use a simple loop to transpose them
        const int threshold = 16;
        if (rows * cols <= threshold)
        {
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    output[j, i] = input[i, j];
                }
            }
            return;
        }

        // Otherwise, recursively split the input and output spans into four sub-spans and transpose them
        var halfRows = rows / 2;
        var halfCols = cols / 2;

        CacheObliviousTranspose(input[..halfRows, ..halfCols], output[..halfRows, ..halfCols]); // Top-left quadrant
        CacheObliviousTranspose(input[..halfRows, halfCols..], output[halfRows.., ..halfCols]); // Top-right quadrant
        CacheObliviousTranspose(input[halfRows.., ..halfCols], output[..halfRows, halfCols..]); // Bottom-left quadrant
        CacheObliviousTranspose(input[halfRows.., halfCols..], output[halfRows.., halfCols..]); // Bottom-right quadrant
    }
}