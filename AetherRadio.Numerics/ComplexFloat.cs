namespace AetherRadio.Numerics;

public struct ComplexFloat
{
    private float _real;
    private float _imag;

    // ReSharper disable once ConvertToAutoPropertyWhenPossible
    public float Real
    {
        get => _real;
        set => _real = value;
    }

    // ReSharper disable once ConvertToAutoPropertyWhenPossible
    public float Imaginary
    {
        get => _imag;
        set => _imag = value;
    }

    public float Magnitude => MathF.Sqrt(_real * _real + _imag * _imag);

    public float Phase => MathF.Atan2(_imag, _real);

    public ComplexFloat Normalized => this / FromPolar(Magnitude, 0);

    public ComplexFloat(float real, float imag)
    {
        _real = real;
        _imag = imag;
    }

    public static ComplexFloat FromPolar(float magnitude, float phase)
    {
        return new ComplexFloat(magnitude * MathF.Cos(phase), magnitude * MathF.Sin(phase));
    }

    public static ComplexFloat operator +(ComplexFloat a, ComplexFloat b)
    {
        return new ComplexFloat(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    public static ComplexFloat operator -(ComplexFloat a, ComplexFloat b)
    {
        return new ComplexFloat(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    public static ComplexFloat operator *(ComplexFloat a, ComplexFloat b)
    {
        return FromPolar(a.Magnitude * b.Magnitude, a.Phase + b.Phase);
    }

    public static ComplexFloat operator /(ComplexFloat a, ComplexFloat b)
    {
        return FromPolar(a.Magnitude / b.Magnitude, a.Phase - b.Phase);
    }


    public void Normalize()
    {
        this = Normalized;
    }
}
