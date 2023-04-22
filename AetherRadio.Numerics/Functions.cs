namespace AetherRadio.Numerics;

public static class Functions
{
    public static bool IsPowerOfTwo(uint value)
    {
        return value != 0 && (value & (value - 1)) == 0;
    }
}

