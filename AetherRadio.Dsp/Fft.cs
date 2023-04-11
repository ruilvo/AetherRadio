namespace AetherRadio.Dsp;

/// <summary>
/// A basic decimation-in-frequency FFT
/// </summary>
public class Fft
{
    private readonly int _size;
    private readonly int _log2Size;
    private readonly float[] _sinTable;
    private readonly float[] _cosTable;

    public Fft(int fftSize)
    {
        _size = fftSize;
        _log2Size = (int)Math.Log(fftSize, 2);
        _sinTable = new float[_size / 2];
        _cosTable = new float[_size / 2];

        FillTwiddleFactors();
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