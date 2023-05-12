using System.Diagnostics;

using CommunityToolkit.HighPerformance;

namespace AetherRadio.Numerics;

public static partial class Functions
{
    public static void BlockTranspose<T>(
        ReadOnlySpan2D<T> A /* input */,
        Span2D<T> B /* output */)
    {
        Debug.Assert(A.Length == B.Length);
        Debug.Assert(A.Height == B.Width);
        Debug.Assert(A.Width == B.Height);
    }
}