using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

public class RectMainProgram
{
    static void Main()
    {
        Random rand = new Random();

        int[] resolutions = {100, 500, 1000};

        foreach (int res in resolutions)
        {
            Screen<Grayscale> scr = new Screen<Grayscale>(res, res, Grayscale.Black);

            Console.WriteLine("Screen size: " + scr.width + " x " + scr.height);
            
            for (int i = 0; i < 5; i++)
            {
                scr.Fill(Grayscale.Black);
                
                Vector2Int p1 = new Vector2Int(rand.Next(0, res), rand.Next(0, res));
                Vector2Int p2 = new Vector2Int(rand.Next(0, res), rand.Next(0, res));
                Vector2Int p3 = new Vector2Int(rand.Next(0, res), rand.Next(0, res));
                Vector2Int p4 = new Vector2Int(rand.Next(0, res), rand.Next(0, res));
                
                scr.DrawLine(Grayscale.Gray, p1, p2);
                scr.DrawLine(Grayscale.Gray, p2, p3);
                scr.DrawLine(Grayscale.Gray, p3, p4);
                scr.DrawLine(Grayscale.Gray, p4, p1);
                
                scr.DrawPoint(Grayscale.White, p1);
                scr.DrawPoint(Grayscale.White, p2);
                scr.DrawPoint(Grayscale.White, p3);
                scr.DrawPoint(Grayscale.White, p4);

                scr.SaveBmp("rectTest/test_" + res + "_" + i + ".bmp");                
            }
        }
    }
}