using System;
using System.Numerics;

public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
{
    public int X;
    public int Y;

    public Vector2Int(int value)
    {
        X = value;
        Y = value;
    }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2Int(Vector2 vector2)
    {
        X = (int) vector2.X;
        Y = (int) vector2.Y;
    }
    
    public bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }

    public static Vector2Int Abs(Vector2Int value)
    {
        return new Vector2Int(Math.Abs(value.X), Math.Abs(value.Y));
    }

    public static Vector2Int operator +(Vector2Int vector1, Vector2Int vector2)
    {
        return new Vector2Int(vector1.X + vector2.X, vector1.Y + vector1.Y);
    }

    public static Vector2Int operator -(Vector2Int vector1, Vector2Int vector2)
    {
        return new Vector2Int(vector1.X - vector2.X, vector1.Y - vector1.Y);
    }

    public static Vector2Int Add(Vector2Int vector1, Vector2Int vector2)
    {
        return vector1 + vector2;
    }

    public static Vector2Int Clamp(Vector2Int value, Vector2Int min, Vector2Int max)
    {
        return new Vector2Int(
            Math.Max(min.X, Math.Min(value.X, max.X)),
            Math.Max(min.Y, Math.Min(value.Y, max.Y))
        );
    }
    
    

    public static Vector2Int One { get; } = new Vector2Int(1, 1);

    public static Vector2Int Zero { get; } = new Vector2Int(0, 0);

    public static Vector2Int UnitX { get; } = new Vector2Int(1, 0);

    public static Vector2Int UnitY { get; } = new Vector2Int(0, 1);
}