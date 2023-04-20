namespace AetherRadio.Numerics;

public struct ComplexFloat
{
    float _real;
    float _imag;

    public float Real
    {
        get { return _real; }
        set { _real = value; }
    }

    public float Imaginary
    {
        get { return _imag; }
        set { _imag = value; }
    }

    public float Magnitude
    {
        get { return MathF.Sqrt(_real * _real + _imag * _imag); }
    }

    public float Phase
    {
        get { return MathF.Atan2(_imag, _real); }
    }

    public ComplexFloat Normalized
    {
        get { return this / FromPolar(Magnitude, 0); }
    }

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
