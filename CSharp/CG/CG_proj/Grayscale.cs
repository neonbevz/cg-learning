using System.Collections.ObjectModel;
using System.Dynamic;
using System.Numerics;

public struct Grayscale : IColor
{
    public byte ByteValue;

//    public Grayscale()
//    {
//        byteValue = 0;
//    }

    public Grayscale(int newValue)
    {
        ByteValue = (byte) CGMath.Clamp(newValue, 0, 255);
    }

    public Grayscale(float newValue)
    {
        ByteValue = (byte) (CGMath.Clamp(newValue, 0f, 1f) * 255);
    }

    public void SetValue(int newValue)
    {
        ByteValue = (byte) CGMath.Clamp(newValue, 0, 255);
    }

    public void SetValue(float newValue)
    {
        ByteValue = (byte) (CGMath.Clamp(newValue, 0f, 1f) * 255);
    }

    public float AsFloat()
    {
        return (float) ByteValue / 255;
    }

    public static Grayscale operator + (Grayscale grayscale1, Grayscale grayscale2)
    {
        return new Grayscale(grayscale1.ByteValue + grayscale2.ByteValue);
    }
    
    public static Grayscale operator - (Grayscale grayscale1, Grayscale grayscale2)
    {
        return new Grayscale(grayscale1.ByteValue - grayscale2.ByteValue);
    }
    
    public static Grayscale operator * (Grayscale grayscale1, Grayscale grayscale2)
    {
        return new Grayscale(grayscale1.AsFloat() * grayscale2.AsFloat());
    }

    public static explicit operator Grayscale(RGB rgb)
    {
        int value = (rgb.byteR + rgb.byteG + rgb.byteB) / 3;
        return new Grayscale(value);
    }
    
    public static Grayscale Black => new Grayscale(0);
    
    public static Grayscale White => new Grayscale(255);
    
    public static Grayscale Gray => new Grayscale(127);
    
    public byte GetOneByteColor()
    {
        return ByteValue;
    }
}