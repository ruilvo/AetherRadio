using System.Diagnostics;

using AetherRadio.Numerics;

namespace AetherRadio.Fft;

public class FftFloat
{
    private readonly uint _length;
    private readonly ComplexFloat[] _twiddleFactors;

    public FftFloat(uint length)
    {
        Debug.Assert(Functions.IsPowerOfTwo(length),
            "The FFT needs to be a power of two!");

        _length = length;
        _twiddleFactors = new ComplexFloat[length / 2];

        // Compute the twiddle factors
        for (uint i = 0; i < _twiddleFactors.Length; i++)
        {
            var angle = -2.0f * MathF.PI * i / _length;
            _twiddleFactors[i] = new ComplexFloat(
                               MathF.Cos(angle), MathF.Sin(angle));
        }
    }

    public void Transform(ReadOnlySpan<ComplexFloat> input, Span<ComplexFloat> output)
    {
        Debug.Assert(input.Length == _length);
        Debug.Assert(output.Length == _length);

        // Copy the input to the output, bit-reversed
        CopyBitReversed(input, output);

        // Perform the FFT
        TransformInternal(output);
    }

    // TODO(ruilvo): Implement CopyBitReversed and TransformInternal.

    private static void CopyBitReversed(ReadOnlySpan<ComplexFloat> input, Span<ComplexFloat> output)
    {
        Debug.Assert(input.Length == output.Length);

        // TODO(ruilvo): Implement the bit-reversal.
    }

    private void TransformInternal(Span<ComplexFloat> data)
    {
        Debug.Assert(data.Length == _length);

        // TODO(ruilvo): Implement the FFT.
    }
}