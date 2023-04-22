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
            _twiddleFactors[i] = ComplexFloat.FromPolar(1, angle);
        }
    }

    public void Transform(ReadOnlySpan<ComplexFloat> input, Span<ComplexFloat> output)
    {
        Debug.Assert(input.Length == _length);
        Debug.Assert(output.Length == _length);

        // Transforms have to be in-place or to not overlap.
        // When the input and output are the same, we can skip the copy.
        if (input != output)
        {
            Debug.Assert(!input.Overlaps(output));
            input.CopyTo(output);
        }

        // Perform the FFT
        TransformDifRadix2(output);
    }

    private void TransformDifRadix2(Span<ComplexFloat> data)
    {
        Debug.Assert(data.Length == _length);

        var twiddleIdxMultiplier = 1;
        for (var blockSize = (int)(_length / 2); blockSize > 1; blockSize /= 2)
        {
            // Each blockSize, skip blockSize elements. 
            for (var blockOffset = 0; blockOffset < _length; blockOffset += blockSize)
            {
                for (var i = 0; i < blockSize; ++i)
                {
                    var idxA = blockOffset + i;
                    var idxB = idxA + blockSize;
                    var idxT = i * twiddleIdxMultiplier;

                    // Perform the butterfly operation
                    // A = a + b
                    // B = (a - b)*W^k_N
                    // where k is a "skip" multiplier, named twiddleIdxMultiplier here,
                    // and N is the length of the FFT, named _length here.
                    // Inspired from: https://digitalsystemdesign.in/wp-content/uploads/2021/10/FFT16.png

                    data[idxA] = data[idxA] + data[idxB];
                    data[idxB + blockSize] = (data[idxA] - data[idxB]) * _twiddleFactors[idxT];
                }
            }

            // Example: on a 16-point FFT, the first iteration will have a blockSize of 8,
            // and thus we'll use the first 8 twiddle factors. The second iteration will
            // have a blockSize of 4, and thus we'll use the twiddle factors 0, 2, 4, 6.
            // The third iteration will have a blockSize of 2, and thus we'll use the
            // twiddle factors 0, 4. The fourth iteration will have a blockSize of 1,
            // and thus we'll use the twiddle factor 0.
            twiddleIdxMultiplier *= 2;
        }
    }
}