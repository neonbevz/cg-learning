public class Grayscale
{
    public int byteValue { get; private set; }

    public Grayscale()
    {
        byteValue = 0;
    }

    public Grayscale(int newValue)
    {
        byteValue = CGMath.Clamp(newValue, 0, 255);
    }

    public Grayscale(float newValue)
    {
        byteValue = (int) (CGMath.Clamp(newValue, 0f, 1f) * 255);
    }

    public void SetValue(int newValue)
    {
        byteValue = CGMath.Clamp(newValue, 0, 255);
    }

    public void SetValue(float newValue)
    {
        byteValue = (int) (CGMath.Clamp(newValue, 0f, 1f) * 255);
    }

    public float AsFloat()
    {
        return (float) byteValue / 255;
    }

    public static Grayscale operator + (Grayscale grayscale1, Grayscale grayscale2)
    {
        return new Grayscale(grayscale1.byteValue + grayscale2.byteValue);
    }
    
    public static Grayscale operator - (Grayscale grayscale1, Grayscale grayscale2)
    {
        return new Grayscale(grayscale1.byteValue - grayscale2.byteValue);
    }
    
    public static Grayscale operator * (Grayscale grayscale1, Grayscale grayscale2)
    {
        return new Grayscale(grayscale1.AsFloat() * grayscale2.AsFloat());
    }
}