namespace AetherRadio.Numerics;

public struct ComplexDouble
{
    double _real;
    double _imag;

    public double Real
    {
        get { return _real; }
        set { _real = value; }
    }

    public double Imaginary
    {
        get { return _imag; }
        set { _imag = value; }
    }

    public double Magnitude
    {
        get { return Math.Sqrt(_real * _real + _imag * _imag); }
    }

    public double Phase
    {
        get { return Math.Atan2(_imag, _real); }
    }

    public ComplexDouble Normalized
    {
        get { return this / FromPolar(Magnitude, 0); }
    }

    public ComplexDouble(double real, double imag)
    {
        _real = real;
        _imag = imag;
    }

    public static ComplexDouble FromPolar(double magnitude, double phase)
    {
        return new ComplexDouble(magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));
    }

    public static ComplexDouble operator +(ComplexDouble a, ComplexDouble b)
    {
        return new ComplexDouble(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    public static ComplexDouble operator -(ComplexDouble a, ComplexDouble b)
    {
        return new ComplexDouble(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    public static ComplexDouble operator *(ComplexDouble a, ComplexDouble b)
    {
        return FromPolar(a.Magnitude * b.Magnitude, a.Phase + b.Phase);
    }

    public static ComplexDouble operator /(ComplexDouble a, ComplexDouble b)
    {
        return FromPolar(a.Magnitude / b.Magnitude, a.Phase - b.Phase);
    }


    public void Normalize()
    {
        this = Normalized;
    }
}