using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geom.Tests;

[TestClass]
public class AngleDegTests
{
    [TestMethod]
    public void DefaultConstructor_Initialization()
    {
        var angle = new AngleDeg();
        Assert.AreEqual(0.0, angle.Degree, 1e-5, "Default constructor should initialize to 0 degrees.");
    }

    [TestMethod]
    public void ParameterizedConstructor_InitializationAndNormalization()
    {
        var angle1 = new AngleDeg(400.0);
        Assert.AreEqual(40.0, angle1.Degree, 1e-5, "Constructor should normalize angle greater than 360 degrees.");

        var angle2 = new AngleDeg(-400.0);
        Assert.AreEqual(-40.0, angle2.Degree, 1e-5, "Constructor should normalize angle less than -360 degrees.");
    }

    [TestMethod]
    public void Abs_ReturnsAbsoluteValue()
    {
        var angle = new AngleDeg(-45.0);
        Assert.AreEqual(45.0, angle.Abs(), 1e-5, "Abs should return the absolute value of the angle.");
    }

    [TestMethod]
    public void Radian_Conversion()
    {
        var angle = new AngleDeg(90.0);
        Assert.AreEqual(Math.PI / 2.0, angle.Radian(), 1e-5, "Radian conversion is incorrect.");
    }

    [TestMethod]
    public void Addition_OperatorWithAngle()
    {
        var angle1 = new AngleDeg(45.0);
        var angle2 = new AngleDeg(30.0);
        var result = angle1 + angle2;
        Assert.AreEqual(75.0, result.Degree, 1e-5, "Addition operator with angle is incorrect.");
    }

    [TestMethod]
    public void Addition_OperatorWithDouble()
    {
        var angle = new AngleDeg(60.0);
        var result = new AngleDeg(angle.Degree + 30.0);
        Assert.AreEqual(90.0, result.Degree, 1e-5, "Addition operator with double is incorrect.");
    }

    // Add more tests for other operators and functions...

    [TestMethod]
    public void ToString_ReturnsStringRepresentation()
    {
        var angle = new AngleDeg(120.0);
        Assert.AreEqual("120", angle.ToString(), "ToString method is incorrect.");
    }

    [TestMethod]
    public void Equality_OperatorWithAngle()
    {
        var angle1 = new AngleDeg(45.0);
        var angle2 = new AngleDeg(45.0);
        Assert.IsTrue(angle1 == angle2, "Equality operator with angle is incorrect.");
    }

    [TestMethod]
    public void Equality_OperatorWithDouble()
    {
        var angle = new AngleDeg(30.0);
        Assert.IsTrue(angle.Degree == 30.0, "Equality operator with double is incorrect.");
    }

    [TestMethod]
    public void Inequality_OperatorWithAngle()
    {
        var angle1 = new AngleDeg(60.0);
        var angle2 = new AngleDeg(90.0);
        Assert.IsTrue(angle1 != angle2, "Inequality operator with angle is incorrect.");
    }

    [TestMethod]
    public void Inequality_OperatorWithDouble()
    {
        var angle = new AngleDeg(45.0);
        Assert.IsTrue(angle.Degree != 60.0, "Inequality operator with double is incorrect.");
    }

    [TestMethod]
    public void IsLeftOf_ReturnsTrue()
    {
        var angle1 = new AngleDeg(30.0);
        var angle2 = new AngleDeg(60.0);
        Assert.IsTrue(angle1.IsLeftOf(angle2), "IsLeftOf should return true.");
    }

    [TestMethod]
    public void IsLeftEqualOf_ReturnsTrue()
    {
        var angle1 = new AngleDeg(90.0);
        var angle2 = new AngleDeg(120.0);
        Assert.IsTrue(angle1.IsLeftEqualOf(angle2), "IsLeftEqualOf should return true.");
    }

    [TestMethod]
    public void IsRightOf_ReturnsTrue()
    {
        var angle1 = new AngleDeg(150.0);
        var angle2 = new AngleDeg(120.0);
        Assert.IsTrue(angle1.IsRightOf(angle2), "IsRightOf should return true.");
    }

    [TestMethod]
    public void IsRightEqualOf_ReturnsTrue()
    {
        var angle1 = new AngleDeg(-45.0);
        var angle2 = new AngleDeg(-90.0);
        Assert.IsTrue(angle1.IsRightEqualOf(angle2), "IsRightEqualOf should return true.");
    }

    [TestMethod]
    public void Cos_ReturnsCosineValue()
    {
        var angle = new AngleDeg(60.0);
        Assert.AreEqual(0.5, angle.Cos(), 1e-5, "Cos method is incorrect.");
    }

    [TestMethod]
    public void Sin_ReturnsSineValue()
    {
        var angle = new AngleDeg(30.0);
        Assert.AreEqual(0.5, angle.Sin(), 1e-5, "Sin method is incorrect.");
    }

    [TestMethod]
    public void Tan_ReturnsTangentValue()
    {
        var angle = new AngleDeg(45.0);
        Assert.AreEqual(1.0, angle.Tan(), 1e-5, "Tan method is incorrect.");
    }
}