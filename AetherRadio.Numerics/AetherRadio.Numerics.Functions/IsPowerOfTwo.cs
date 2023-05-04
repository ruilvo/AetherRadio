namespace AetherRadio.Numerics;

public static partial class Functions
{
    public static bool IsPowerOfTwo(int value)
    {
        return value != 0 && (value & (value - 1)) == 0;
    }
}