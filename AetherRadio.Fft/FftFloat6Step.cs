using System.Diagnostics;

using AetherRadio.Numerics;

namespace AetherRadio.Fft;

public class FftFloat6Step
{
    private readonly int _length;

    public FftFloat6Step(int length)
    {
        Debug.Assert(Functions.IsPowerOfTwo(length),
            "The FFT needs to be a power of two!");

        _length = length;
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
        Transform6Step(output);
    }

    private void Transform6Step(Span<ComplexFloat> data)
    {
        Debug.Assert(data.Length == _length);

        // Scratch area
        var buffer = new ComplexFloat[_length];

        // TODO(ruilvo): Finish the implementation
    }
}
