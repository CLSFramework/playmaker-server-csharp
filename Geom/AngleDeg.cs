namespace Geom;

public class AngleDeg
{
    public const double EPSILON = 1.0e-5;
    public const double PI = Math.PI;
    public const double TWO_PI = 2.0 * Math.PI;
    public const double DEG2RAD = Math.PI / 180.0;
    public const double RAD2DEG = 180.0 / Math.PI;

    public AngleDeg()
    {
        Degree = 0.0;
    }

    public AngleDeg(double deg)
    {
        Degree = deg;
        Normalize();
    }

    private AngleDeg(AngleDeg left)
    {
        Degree = left.Degree;
    }

    public double Degree { get; private set; }

    public double Abs()
    {
        return Math.Abs(Degree);
    }

    public double Radian()
    {
        return Degree * DEG2RAD;
    }

    public AngleDeg Normalize()
    {
        if (Degree < -360.0 || 360.0 < Degree) Degree = Degree % 360.0;

        if (Degree < -180.0) Degree += 360.0;

        if (Degree > 180.0) Degree -= 360.0;

        return this;
    }

    public static AngleDeg operator +(AngleDeg angleA, AngleDeg angleB)
    {
        return new AngleDeg(angleB.Degree + angleA.Degree).Normalize();
    }

    public static AngleDeg operator -(AngleDeg angleA, AngleDeg angleB)
    {
        return new AngleDeg(angleA.Degree - angleB.Degree).Normalize();
    }

    public static AngleDeg operator *(AngleDeg angle, double scalar)
    {
        return new AngleDeg(angle.Degree * scalar).Normalize();
    }

    public static AngleDeg operator /(AngleDeg angle, double scalar)
    {
        if (Math.Abs(scalar) < EPSILON) return angle;

        return new AngleDeg(angle.Degree / scalar).Normalize();
    }

    public static bool operator ==(AngleDeg angleA, AngleDeg angleB)
    {
        return Math.Abs(angleA.Degree - angleB.Degree) < EPSILON;
    }

    public static bool operator !=(AngleDeg angleA, AngleDeg angleB)
    {
        return !(angleA == angleB);
    }

    public bool IsLeftOf(AngleDeg angle)
    {
        var diff = angle.Degree - Degree;
        return (0.0 < diff && diff < 180.0) || diff < -180.0;
    }

    public bool IsLeftEqualOf(AngleDeg angle)
    {
        var diff = angle.Degree - Degree;
        return (0.0 <= diff && diff < 180.0) || diff < -180.0;
    }

    public bool IsRightOf(AngleDeg angle)
    {
        var diff = Degree - angle.Degree;
        return (0.0 < diff && diff < 180.0) || diff < -180.0;
    }

    public bool IsRightEqualOf(AngleDeg angle)
    {
        var diff = Degree - angle.Degree;
        return (0.0 <= diff && diff < 180.0) || diff < -180.0;
    }

    public double Cos()
    {
        return Math.Cos(Degree * DEG2RAD);
    }

    public double Sin()
    {
        return Math.Sin(Degree * DEG2RAD);
    }

    public double Tan()
    {
        return Math.Tan(Degree * DEG2RAD);
    }

    public bool IsWithin(AngleDeg left, AngleDeg right)
    {
        if (left.IsLeftEqualOf(right))
        {
            if (left.IsLeftEqualOf(this) && IsLeftEqualOf(right)) return true;
        }
        else
        {
            if (IsLeftEqualOf(right) || left.IsLeftEqualOf(this)) return true;
        }

        return false;
    }

    public void SinMinMax(double angleErr, out double minSin, out double maxSin)
    {
        if (angleErr < 0.0 || angleErr > 180.0)
        {
            Console.WriteLine("AngleDeg::SinMinMax() invalid error range. " + angleErr);
            minSin = -1.0;
            maxSin = 1.0;
            return;
        }

        var minDir = Degree - angleErr;
        var maxDir = Degree + angleErr;

        var sol = new List<double>();

        if ((minDir < -90.0 && -90.0 < maxDir) || (minDir < 270.0 && 270.0 < maxDir)) sol.Add(-1.0);

        if ((minDir < 90.0 && 90.0 < maxDir) || (minDir < -270.0 && -270.0 < maxDir)) sol.Add(1.0);

        sol.Add(SinDeg(minDir));
        sol.Add(SinDeg(maxDir));

        minSin = sol.Min();
        maxSin = sol.Max();
    }

    public void CosMinMax(double angleErr, out double minCos, out double maxCos)
    {
        if (angleErr < 0.0 || angleErr > 180.0)
        {
            Console.WriteLine("AngleDeg::CosMinMax() invalid error range. " + angleErr);
            minCos = -1.0;
            maxCos = 1.0;
            return;
        }

        var minDir = Degree - angleErr;
        var maxDir = Degree + angleErr;

        var sol = new List<double>();

        if (minDir < -180.0 && -180.0 < maxDir) sol.Add(-1.0);

        if (minDir < 0.0 && 0.0 < maxDir) sol.Add(1.0);

        sol.Add(CosDeg(minDir));
        sol.Add(CosDeg(maxDir));

        minCos = sol.Min();
        maxCos = sol.Max();
    }

    public static double NormalizeAngle(double dir)
    {
        if (dir < -360.0 || 360.0 < dir) dir = dir % 360.0;

        if (dir < -180.0) dir += 360.0;

        if (dir > 180.0) dir -= 360.0;

        return dir;
    }

    public static double RadToDeg(double rad)
    {
        return rad * RAD2DEG;
    }

    public static double DegToRad(double deg)
    {
        return deg * DEG2RAD;
    }

    public static double CosDeg(double deg)
    {
        return Math.Cos(DegToRad(deg));
    }

    public static double SinDeg(double deg)
    {
        return Math.Sin(DegToRad(deg));
    }

    public static double TanDeg(double deg)
    {
        return Math.Tan(DegToRad(deg));
    }

    public static
        double AcosDeg(double cosine)
    {
        return cosine >= 1.0 ? 0.0 : cosine <= -1.0 ? 180.0 : RadToDeg(Math.Acos(cosine));
    }

    public static double AsinDeg(double sine)
    {
        return sine >= 1.0 ? 90.0 : sine <= -1.0 ? -90.0 : RadToDeg(Math.Asin(sine));
    }

    public static double AtanDeg(double tangent)
    {
        return RadToDeg(Math.Atan(tangent));
    }

    public static double Atan2Deg(double y, double x)
    {
        return x == 0.0 && y == 0.0 ? 0.0 : RadToDeg(Math.Atan2(y, x));
    }

    public static AngleDeg Bisect(AngleDeg left, AngleDeg right)
    {
        var result = new AngleDeg(left);
        var rel = right - left;
        var halfDeg = rel.Degree * 0.5;
        result += new AngleDeg(halfDeg);

        if (left.IsLeftOf(right))
            return result;
        return result += new AngleDeg(180.0);
    }

    public override string ToString()
    {
        return Degree.ToString();
    }
}