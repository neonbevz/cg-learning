using System;
using System.Drawing;
using System.Drawing.Imaging;


public class Screen<T> where T : IColor
{
    public int width { get; private set; }
    public int height { get; private set; }

    private T[,] screen;

    public Screen(int screenWidth, int screenHeight, T background)
    {
        width = screenWidth;
        height = screenHeight;

        screen = new T[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                screen[x, y] = background;
            }
        }
    }

    public void Fill(T color)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                screen[x, y] = color;
            }
        }
    }

    public void DrawPoint(T color, Vector2Int pixel)
    {
        if (pixel.X < 0 || pixel.X >= width)
            return;
        if (pixel.Y < 0 || pixel.Y >= height)
            return;
        
        screen[pixel.X, pixel.Y] = color;
    }

    public void DrawLine(T color, Vector2Int pixel1, Vector2Int pixel2)
    {
        Vector2Int from;
        Vector2Int to;
        
        if (pixel1.X == pixel2.X)
        {
            if (pixel2.Y > pixel1.Y)
            {
                from = pixel1;
                to = pixel2;
            }
            else
            {
                from = pixel2;
                to = pixel1;
            }

            for (int y = from.Y; y < to.Y; y++)
            {
                DrawPoint(color, new Vector2Int(from.X, y));
            }
            return;
        }
//        else if (pixel1.X > pixel2.X)
//        {
//            realX1 = x2;
//            realY1 = y2;
//            realX2 = x1;
//            realY2 = y1;
//        }

        int deltaX = pixel2.X - pixel1.X;
        int deltaY = pixel2.Y - pixel1.Y;

        bool horizontal = Math.Abs(deltaY) <= Math.Abs(deltaX);

        if (horizontal)
        {
            if (pixel2.X > pixel1.X)
            {
                from = pixel1;
                to = pixel2;
            }
            else
            {
                from = pixel2;
                to = pixel1;
            }
        }
        else
        {
            if (pixel2.Y > pixel1.Y)
            {
                from = pixel1;
                to = pixel2;
            }
            else
            {
                from = pixel2;
                to = pixel1;
            }
        }
        
        double stepDelta = horizontal ? (double) deltaY / deltaX : (double) deltaX / deltaY;

        double totalDelta = 0d;

        int coordB = horizontal ? from.Y : from.X;
        int rangeStart = horizontal ? from.X : from.Y;
        int rangeEnd = horizontal ? to.X : to.Y;

        for (int coordA = rangeStart; coordA < rangeEnd; coordA++)
        {
            if (horizontal)
                DrawPoint(color, new Vector2Int(coordA, coordB));
            else
                DrawPoint(color, new Vector2Int(coordB, coordA));

            totalDelta += stepDelta;

            if (Math.Abs(totalDelta) >= 0.5d)
            {
                coordB += stepDelta > 0 ? 1 : -1;
                totalDelta += totalDelta < 0 ? 1 : -1;
            }
        }        
    }

//    public void DrawTri();
    
    public int[,] AsIntArray()
    {
        int[,] arr = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                arr[x, y] = screen[x, y].GetOneByteColor();
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
                arr[y, x] = screen[x, y].GetOneByteColor();
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
                pixels[y * width + x] = screen[x, y].GetOneByteColor();
            }
        }
        
        
        Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            
        ColorPalette pal = bmp.Palette;
        
        for (int i = 0; i < 256; i++)
        {
            pal.Entries[i] = Color.FromArgb(255, i, i, i);
        }
        
        bmp.Palette = pal;

        BitmapData bmData =
            bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly,
                PixelFormat.Format8bppIndexed);
        
        System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmData.Scan0, bmData.Stride * bmData.Height);

        bmp.UnlockBits(bmData);
        
        bmp.Save(filename);
    }
}