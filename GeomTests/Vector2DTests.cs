using Geom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Vector2DTests
{
    [TestMethod]
    public void DefaultConstructor_InitializedToZero()
    {
        var vector = new Vector2D();
        Assert.AreEqual(0.0, vector.X);
        Assert.AreEqual(0.0, vector.Y);
    }

    [TestMethod]
    public void ParameterizedConstructor_InitializedCorrectly()
    {
        var vector = new Vector2D(3.0, 4.0);
        Assert.AreEqual(3.0, vector.X);
        Assert.AreEqual(4.0, vector.Y);
    }

    [TestMethod]
    public void IsValid_WhenValid_ReturnsTrue()
    {
        var vector = new Vector2D(1.0, 2.0);
        Assert.IsTrue(vector.IsValid());
    }

    [TestMethod]
    public void IsValid_WhenInvalid_ReturnsFalse()
    {
        var vector = Vector2D.INVALIDATED;
        Assert.IsFalse(vector.IsValid());
    }

    [TestMethod]
    public void Assign_ModifiesVector()
    {
        var vector = new Vector2D(1.0, 2.0);
        vector.Assign(3.0, 4.0);
        Assert.AreEqual(3.0, vector.X);
        Assert.AreEqual(4.0, vector.Y);
    }

    [TestMethod]
    public void SetPolar_SetsPolarCoordinates()
    {
        var vector = new Vector2D();
        vector.SetPolar(5.0, new AngleDeg(30.0));
        Assert.AreEqual(5.0 * Math.Cos(30.0 * AngleDeg.DEG2RAD), vector.X, 1e-6);
        Assert.AreEqual(5.0 * Math.Sin(30.0 * AngleDeg.DEG2RAD), vector.Y, 1e-6);
    }

    [TestMethod]
    public void Invalidate_InvalidatesVector()
    {
        var vector = new Vector2D(1.0, 2.0);
        vector.Invalidate();
        Assert.AreEqual(Vector2D.ERROR_VALUE, vector.X);
        Assert.AreEqual(Vector2D.ERROR_VALUE, vector.Y);
    }

    // Add more tests for other methods and operators...

    [TestMethod]
    public void Rotate_AppliesRotation()
    {
        var vector = new Vector2D(1.0, 0.0);
        vector.Rotate(90.0);
        Assert.AreEqual(0.0, vector.X, 1e-6);
        Assert.AreEqual(1.0, vector.Y, 1e-6);
    }

    [TestMethod]
    public void Equals_WhenEqual_ReturnsTrue()
    {
        var vector1 = new Vector2D(1.0, 2.0);
        var vector2 = new Vector2D(1.0, 2.0);
        Assert.IsTrue(vector1.Equals(vector2));
    }

    [TestMethod]
    public void Equals_WhenNotEqual_ReturnsFalse()
    {
        var vector1 = new Vector2D(1.0, 2.0);
        var vector2 = new Vector2D(3.0, 4.0);
        Assert.IsFalse(vector1.Equals(vector2));
    }

    [TestMethod]
    public void ToString_ReturnsStringRepresentation()
    {
        var vector = new Vector2D(3.0, 4.0);
        Assert.AreEqual("(3, 4)", vector.ToString());
    }
}