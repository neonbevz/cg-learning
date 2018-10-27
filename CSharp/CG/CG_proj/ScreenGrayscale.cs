using System;
using System.Drawing;
using System.Drawing.Imaging;

public class ScreenGrayscale
{
    public int width { get; private set; }
    public int height { get; private set; }

    private Grayscale[,] screen;

    public ScreenGrayscale(int screenWidth, int screenHeight, Grayscale bg = null)
    {
        width = screenWidth;
        height = screenHeight;

        screen = new Grayscale[width, height];

        int bgValue = (bg is null) ? 0 : bg.byteValue;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                screen[x, y] = new Grayscale(bgValue);
            }
        }
    }

    public void Fill(int color)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                screen[x, y].SetValue(color);
            }
        }
    }

    public void DrawPoint(int color, int x, int y)
    {
        if (x < 0 || x >= width)
            return;
        if (y < 0 || y >= height)
            return;
        
        screen[x, y].SetValue(color);
    }

    public void DrawLine(int color, int x1, int y1, int x2, int y2)
    {
        int realX1 = x1;
        int realY1 = y1;
        int realX2 = x2;
        int realY2 = y2;
        
        if (x1 == x2)
        {
            if (y1 == y2)
            {
                realY1 = y2;
                realY2 = y1;
            }

            for (int y = realY1; y < realY2; y++)
            {
                screen[x1, y].SetValue(color);
            }
            return;
        }
        else if (x1 > x2)
        {
            realX1 = x2;
            realY1 = y2;
            realX2 = x1;
            realY2 = y1;
        }

        int deltaX = realX2 - realX1;
        int deltaY = realY2 - realY1;

        bool horizontal = Math.Abs(deltaY) <= Math.Abs(deltaX);

        if (!horizontal && realY1 > realY2)
        {
            if (x1 > x2)
            {
                realX1 = x1;
                realY1 = y1;
                realX2 = x2;
                realY2 = y2;
            }
            else
            {
                realX1 = x2;
                realY1 = y2;
                realX2 = x1;
                realY2 = y1;
            }
        }

        double stepDelta = (horizontal) ? (double) deltaY / deltaX : (double) deltaX / deltaY;

        double totalDelta = 0d;

        int coordB = horizontal ? realY1 : realX1;
        int rangeStart = horizontal ? realX1 : realY1;
        int rangeEnd = horizontal ? realX2 : realY2;

        for (int coordA = rangeStart; coordA < rangeEnd; coordA++)
        {
            if (horizontal)
                screen[coordA, coordB].SetValue(color);
            else
                screen[coordB, coordA].SetValue(color);

            totalDelta += stepDelta;

            if (Math.Abs(totalDelta) >= 0.5d)
            {
                coordB += (stepDelta > 0) ? 1 : -1;
                totalDelta += (totalDelta < 0) ? 1 : -1;
            }
        }
        
        return;
    }
    
    public int[,] AsIntArray()
    {
        int[,] arr = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                arr[x, y] = screen[x, y].byteValue;
            }
        }

        return arr;
    }

    public int[,] AsIntArrayTranspose()
    {
        int[,] arr = new int[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                arr[y, x] = screen[x, y].byteValue;
            }
        }

        return arr;
    }

    public void SaveBmp(string filename)
    {
//        int[,] transpose = AsIntArray();
        
        byte[] pixels = new byte[width * height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pixels[y * width + x] = screen[x, y].byteValue;
            }
        }
        
        Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            
        ColorPalette pal = bmp.Palette;
        
        for (int i = 0; i < 256; i++)
        {
            pal.Entries[i] = Color.FromArgb(255, i, i, i);
        }
        
        bmp.Palette = pal;

        System.Drawing.Imaging.BitmapData bmData =
            bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
        
        System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmData.Scan0, bmData.Stride * bmData.Height);

        bmp.UnlockBits(bmData);
        
        bmp.Save(filename);
    }
}