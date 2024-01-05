namespace Geom;

public class Vector2D
{
    public static readonly double EPSILON = 1.0e-6;
    public static readonly double ERROR_VALUE = double.MaxValue;
    public static readonly Vector2D INVALIDATED = new(ERROR_VALUE, ERROR_VALUE);

    public double X { get; set; }
    public double Y { get; set; }

    public Vector2D()
    {
        X = 0.0;
        Y = 0.0;
    }

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    private Vector2D(Vector2D vector2D)
    {
        X = vector2D.X;
        Y = vector2D.Y;
    }

    public bool IsValid()
    {
        return X != ERROR_VALUE && Y != ERROR_VALUE;
    }

    public Vector2D Assign(double xx, double yy)
    {
        X = xx;
        Y = yy;
        return this;
    }

    public Vector2D SetPolar(double radius, AngleDeg dir)
    {
        X = radius * dir.Cos();
        Y = radius * dir.Sin();
        return this;
    }

    public Vector2D Invalidate()
    {
        X = ERROR_VALUE;
        Y = ERROR_VALUE;
        return this;
    }

    public double R2()
    {
        return X * X + Y * Y;
    }

    public double R()
    {
        return Math.Sqrt(R2());
    }

    public double Norm()
    {
        return R();
    }

    public double Norm2()
    {
        return R2();
    }

    public double Length()
    {
        return R();
    }

    public double Length2()
    {
        return R2();
    }

    public AngleDeg Th()
    {
        return new AngleDeg(AngleDeg.Atan2Deg(Y, X));
    }

    public AngleDeg Dir()
    {
        return Th();
    }

    public Vector2D Abs()
    {
        return new Vector2D(Math.Abs(X), Math.Abs(Y));
    }

    public double AbsX()
    {
        return Math.Abs(X);
    }

    public double AbsY()
    {
        return Math.Abs(Y);
    }

    public Vector2D Add(Vector2D v)
    {
        X += v.X;
        Y += v.Y;
        return this;
    }

    public Vector2D Add(double xx, double yy)
    {
        X += xx;
        Y += yy;
        return this;
    }

    public Vector2D Scale(double scalar)
    {
        X *= scalar;
        Y *= scalar;
        return this;
    }

    //operator+,-,*,/ are defined as static methods

    public static Vector2D operator +(Vector2D lhs, Vector2D rhs)
    {
        return new Vector2D(lhs.X + rhs.X, lhs.Y + rhs.Y);
    }

    public static Vector2D operator -(Vector2D lhs, Vector2D rhs)
    {
        return new Vector2D(lhs.X - rhs.X, lhs.Y - rhs.Y);
    }

    public static Vector2D operator *(Vector2D vector, double scalar)
    {
        return new Vector2D(vector.X * scalar, vector.Y * scalar);
    }

    public static Vector2D operator /(Vector2D vector, double scalar)
    {
        if (Math.Abs(scalar) > EPSILON) return new Vector2D(vector.X / scalar, vector.Y / scalar);
        return vector; // Avoid division by zero
    }

    public double Dist2(Vector2D p)
    {
        return Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2);
    }

    public double Dist(Vector2D p)
    {
        return Math.Sqrt(Dist2(p));
    }

    public double Dist2(double xx, double yy)
    {
        return Math.Pow(X - xx, 2) + Math.Pow(Y - yy, 2);
    }

    public double Dist(double xx, double yy)
    {
        return Math.Sqrt(Dist2(xx, yy));
    }

    public Vector2D Reverse()
    {
        X = -X;
        Y = -Y;
        return this;
    }

    public Vector2D ReversedVector()
    {
        return new Vector2D(this).Reverse();
    }

    public Vector2D SetLength(double len)
    {
        var mag = R();
        if (Math.Abs(mag) < EPSILON) return this;
        return Scale(len / mag);
    }

    public Vector2D SetLengthVector(double len)
    {
        return new Vector2D(this).SetLength(len);
    }

    public Vector2D Normalize()
    {
        return SetLength(1.0);
    }

    public Vector2D NormalizedVector()
    {
        return new Vector2D(this).Normalize();
    }

    public Vector2D Rotate(double deg)
    {
        var c = Math.Cos(deg * AngleDeg.DEG2RAD);
        var s = Math.Sin(deg * AngleDeg.DEG2RAD);
        return Assign(X * c - Y * s, X * s + Y * c);
    }

    public Vector2D Rotate(AngleDeg angle)
    {
        return Rotate(angle.Degree);
    }

    public Vector2D RotatedVector(double deg)
    {
        return new Vector2D(this).Rotate(deg);
    }

    public Vector2D RotatedVector(AngleDeg angle)
    {
        return new Vector2D(this).Rotate(angle.Degree);
    }

    public Vector2D SetDir(AngleDeg dir)
    {
        var radius = R();
        X = radius * dir.Cos();
        Y = radius * dir.Sin();
        return this;
    }

    public double InnerProduct(Vector2D v)
    {
        return X * v.X + Y * v.Y;
    }

    public double OuterProduct(Vector2D v)
    {
        return X * v.Y - Y * v.X;
    }

    public bool Equals(Vector2D other)
    {
        return X == other.X && Y == other.Y;
    }

    public bool EqualsWeakly(Vector2D other)
    {
        return Math.Abs(X - other.X) < EPSILON && Math.Abs(Y - other.Y) < EPSILON;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}