public class RGB
{
    public int byteR { get; private set; }
    public int byteG { get; private set; }
    public int byteB { get; private set; }

    public RGB()
    {
        byteR = 0;
        byteG = 0;
        byteB = 0;
    }
    
    public RGB(int newR, int newG, int newB)
    {
        SetValues(newR, newG, newB);
    }
    
    public RGB(float newR, float newG, float newB)
    {
        SetValues(newR, newG, newB);

    }

    public void SetValues(int newR, int newG, int newB)
    {
        byteR = CGMath.Clamp(newR, 0, 255);
        byteG = CGMath.Clamp(newG, 0, 255);
        byteB = CGMath.Clamp(newB, 0, 255);
    }
    
    public void SetValues(float newR, float newG, float newB)
    {
        byteR = (int) CGMath.Clamp(newR, 0f, 1f) * 255;
        byteG = (int) CGMath.Clamp(newG, 0f, 1f) * 255;
        byteB = (int) CGMath.Clamp(newB, 0f, 1f) * 255;
    }

    public float[] AsFloats()
    {
        float[] arr = new float[3];

        arr[0] = (float) byteR / 255;
        
        return arr;
    }

    public static RGB operator + (RGB rgb1, RGB rgb2)
    {
        return new RGB(
            rgb1.byteR + rgb2.byteR, 
            rgb1.byteG + rgb2.byteG, 
            rgb1.byteB + rgb2.byteB);
    }
    
    public static RGB operator - (RGB rgb1, RGB rgb2)
    {
        return new RGB(
            rgb1.byteR - rgb2.byteR, 
            rgb1.byteG - rgb2.byteG, 
            rgb1.byteB - rgb2.byteB);
    }

    public static RGB operator * (RGB rgb1, RGB rgb2)
    {
        float[] floatValues1 = rgb1.AsFloats();
        float[] floatValues2 = rgb2.AsFloats();

        return new RGB(
            floatValues1[0] * floatValues2[0], 
            floatValues1[1] * floatValues2[1], 
            floatValues1[2] * floatValues2[2]);
    }
} 