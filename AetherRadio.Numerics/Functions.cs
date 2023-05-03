﻿using System.Diagnostics;

using CommunityToolkit.HighPerformance;

namespace AetherRadio.Numerics;

public static class Functions
{
    public static bool IsPowerOfTwo(int value)
    {
        return value != 0 && (value & (value - 1)) == 0;
    }

    public static void RecTranspose<T>(
    ReadOnlySpan2D<T> A /* input */,
    Span2D<T> B /* output */)
    {
        Debug.Assert(A.Length == B.Length);
        Debug.Assert(A.Height == B.Width);
        Debug.Assert(A.Width == B.Height);

        var length = A.Length; // aka B.Length.

        // TODO(ruilvo): Study the length where the naive algorithm is faster.
        if (length == 1)
        {
            A.CopyTo(B);
            return;
        }

        var nCols = A.Width;
        var mRows = A.Height;

        if (nCols >= mRows)
        {
            int partitionPoint = nCols / 2;

            var A0 = A[.., ..partitionPoint];
            var A1 = A[.., partitionPoint..];
            var B0 = B[..partitionPoint, ..];
            var B1 = B[partitionPoint.., ..];

            RecTranspose(A0, B0);
            RecTranspose(A1, B1);
        }
        else
        {
            int partitionPoint = mRows / 2;

            var A0 = A[..partitionPoint, ..];
            var A1 = A[partitionPoint.., ..];
            var B0 = B[.., ..partitionPoint];
            var B1 = B[.., partitionPoint..];

            RecTranspose(A0, B0);
            RecTranspose(A1, B1);
        }
    }
}

