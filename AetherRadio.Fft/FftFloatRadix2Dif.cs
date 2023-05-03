using System.Diagnostics;

using AetherRadio.Numerics;

namespace AetherRadio.Fft;

public class FftFloatRadix2Dif
{
    private readonly int _length;
    private readonly ComplexFloat[] _twiddleFactors;

    public FftFloatRadix2Dif(int length)
    {
        Debug.Assert(Functions.IsPowerOfTwo(length),
            "The FFT needs to be a power of two!");

        _length = length;
        _twiddleFactors = new ComplexFloat[_length / 2];

        // Compute the twiddle factors
        for (var i = 0; i < _twiddleFactors.Length; i++)
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

        // ---------------------------------------------------------------------

        // Plain english write-up of the algorithm:

        // References:
        // https://www.researchgate.net/profile/Davinder-Sharma/publication/235678991/figure/fig5/AS:666775184211975@1535983000324/Signal-Flow-Diagram-for-16-point-Radix-2-DIF-FFT-Algorithm.png
        // https://digitalsystemdesign.in/wp-content/uploads/2021/10/FFT16.png
        // The first shows the multiplications factors much better,
        // the second shows the twiddle-factor reuse.

        // We can reuse the twiddle factors for every stage of the FFT by using a stride.
        // Example, in a 16-point, radix-2, DIF FFT, in the first stage, we have
        // _twiddleFactors[0..8]. In the second stage, we have _twiddleFactors[0, 2, 4, 6].
        // In the third stage, we have _twiddleFactors[0, 4]. Finally, in the fourth stage,
        // we have _twiddleFactors[0].

        // In this radix-2, DIF FFT, at every stage, the result at some index k is:
        // k % currentFftSize < (currentFftSize / 2) ?
        //  data[k] + data[k + currentFftSize / 2] :
        //      (-1.0 * data[k] + data[k - currentFftSize / 2]) * W^k_N,
        // where W^k_N is the twiddle factor at index [(k % halfFftSize) * currentTwiddleStride].

        // Since we have to replace the values in the array, we need a temporary buffer.
        // We have to switch between the buffer and the output, and if we end up with the FFT
        // in the buffer, we have to copy the result back to the output.

        // ---------------------------------------------------------------------

        // Buffer for the FFT
        var buffer = new ComplexFloat[_length]; // TODO(ruilvo): Make this buffer a member of the class.

        // The current FFT size.
        var currentFftSize = _length;

        // The current stride for the twiddle factors.
        var currentTwiddleStride = 1;

        Span<ComplexFloat> inBuffer = data;
        Span<ComplexFloat> outBuffer = buffer;

        // Let's perform the FFT, stage by stage, until we reach the last stage (size 2).
        while (currentFftSize > 1)
        {
            var halfFftSize = currentFftSize / 2;

            // We iterate over the input buffer, and write the result to the output buffer.
            for (var idx = 0; idx < currentFftSize; ++idx)
            {
                outBuffer[idx] = (idx & (currentFftSize - 1)) < halfFftSize ?
                    inBuffer[idx] + inBuffer[idx + halfFftSize] :
                    (-inBuffer[idx] + inBuffer[idx - halfFftSize])
                        * _twiddleFactors[(idx % halfFftSize) * currentTwiddleStride];
            }
            // We half the FFT size at each iteration, double the stride, and swap the buffers.
            currentFftSize /= 2;
            currentTwiddleStride *= 2;
            // Span<T> can't be used for tuple-based swapping, so we use a temporary variable.
            var temp = inBuffer;
            inBuffer = outBuffer;
            outBuffer = temp;
        }

        // At the end, we check whether we have to copy the result back to the `data` buffer.
        // Remember that we swapped the buffers at the end of the last iteration,
        // so we compare against inBuffer.
        if (inBuffer != data)
        {
            inBuffer.CopyTo(data);
        }
    }
}