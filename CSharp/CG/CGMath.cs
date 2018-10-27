using System;

public static class CGMath
{
    public static int Clamp(int value, int lower, int upper)
    {
        return Math.Max(lower, Math.Min(value, upper));
    }
    
    public static float Clamp(float value, float lower, float upper)
    {
        return Math.Max(lower, Math.Min(value, upper));
    }
    
    public static double Clamp(double value, double lower, double upper)
    {
        return Math.Max(lower, Math.Min(value, upper));
    }
}