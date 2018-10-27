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

//    public void DrawLine(int color, int x1, int y1, int x2, int y2)
//    {
//        int realX1 = x1;
//        int realY1 = y1;
//        int realX2 = x2;
//        int realY2 = y2;
//        
//        if (x1 == x2)
//        {
//            if (y1 == y2)
//            {
//                realY1 = y2;
//                realY2 = y1;
//            }
//
//            for (int y = realY1; y < realY2; y++)
//            {
//                screen[x1, y].SetValue(color);
//            }
//            return;
//        }
//        else if (x1 > x2)
//        {
//            realX1 = x2;
//            realY1 = y2;
//            realX2 = x1;
//            realY2 = y1;
//        }
//
//        int deltaX = realX2 - realX1;
//        int deltaY = realY2 - realY1;
//
//        bool horizontal = Math.Abs(deltaY) <= Math.Abs(deltaX);
//
//        if (!horizontal && realY1 > realY2)
//        {
//            
//        }
        
        
//        if a[0] == b[0]:
//        if a[1] > b[1]:
//        a, b = b, a
//        for y in range(a[1], b[1]):
//        matrix[a[0]][y] = color
//
//        elif a[0] > b[0]:
//        a, b = b, a
//
//            delta_x = b[0] - a[0]
//        delta_y = b[1] - a[1]
//
//        horizontal = abs(delta_y) <= abs(delta_x)
//
//        if not horizontal and a[1] > b[1]:
//        a, b = b, a
//
//            step_delta = delta_y / delta_x if horizontal else delta_x / delta_y
//
//        total_delta = 0
//
//        coord_b = a[1] if horizontal else a[0]
//        range_start, range_end = (a[0], b[0]) if horizontal else (a[1], b[1])
//
//        for coord_a in range(range_start, range_end):
//        if horizontal:
//        matrix[coord_a][coord_b] = color
//        else:
//        matrix[coord_b][coord_a] = color
//
//        total_delta += step_delta
//        if abs(total_delta) >= 0.5:
//        coord_b += 1 if step_delta > 0 else -1
//        total_delta += 1 if total_delta < 0 else -1

//        
//        return;
//    }
    
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