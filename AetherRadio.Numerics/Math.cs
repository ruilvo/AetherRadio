namespace AetherRadio.Numerics;

public static class Math
{
    public static bool IsPowerOfTwo(uint value)
    {
        return (value & (value - 1)) == 0;
    }   
}

