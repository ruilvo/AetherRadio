using System.Drawing;
using System.Numerics;

namespace AetherRadio.Dsp;

/// <summary>
/// A basic decimation-in-frequency FFT
/// </summary>
public class FftDouble
{
    private readonly int _size;
    private readonly int[] bitReverseIndices;
    private readonly Complex[] twiddleFactors;

    public FftDouble(int size)
    {
        if (!IsPowerOfTwo(size))
        {
            throw new ArgumentException("FFT size must be a power of two");
        }

        _size = size;

        // Compute bit-reversed indices
        bitReverseIndices = new int[_size];
        for (int i = 0; i < n_size; i++)
        {
            bitReverseIndices[i] = BitReverse(i, _size);
        }
    }

    private static bool IsPowerOfTwo(int x)
    {
        return (x & (x - 1)) == 0;
    }

    private static int BitReverse(int n, int bits)
    {
        var reversed = 0;
        for (var i = 0; i < bits; i++)
        {
            reversed = (reversed << 1) | (n & 1);
            n >>= 1;
        }
        return reversed;
    }

    private void FillTwiddleFactors()
    {
        for (var i = 0; i < _size / 2; i++)
        {
            _sinTable[i] = (float)Math.Sin(-2 * Math.PI * i / _size);
            _cosTable[i] = (float)Math.Cos(-2 * Math.PI * i / _size);
        }
    }

    private int ReverseBits(int idx)
    {
        var reversed = 0;
        for (var i = 0; i < _log2Size; i++)
        {
            reversed = (reversed << 1) | (idx & 1);
            idx >>= 1;
        }
        return reversed;
    }

    public void ExecuteInPlace(Span<float> data)
    {
        // Swap data around to be in bit-reversed order
        for (var i = 0; i < _size; i++)
        {
            var j = ReverseBits(i);
            if (j <= i) continue;
            (data[i], data[j]) = (data[j], data[i]);
        }

        // Cooley-Tukey decimation-in-frequency
        for (var blockSize = 2; blockSize <= _size; blockSize *= 2)
        {
            var halfBlockSize = blockSize / 2;
            for (var i = 0; i < _size; i += blockSize)
            {
                for (int j = i, k = 0; j < i + halfBlockSize; j++, k += _size / blockSize)
                {
                    var t = data[j + halfBlockSize] * _cosTable[k] - data[j + halfBlockSize] * _sinTable[k];
                    data[j + halfBlockSize] = data[j] - t;
                    data[j] += t;
                }
            }
        }
    }
}