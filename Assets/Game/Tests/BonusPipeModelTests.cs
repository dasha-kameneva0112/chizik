using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class BonusPipeModelTests
{
    [Test]
    public void UseBrushWithPositiveCount()
    {
        var model = new BonusPipeModel(2, 0);
        model.ToggleBrush();

        Assert.IsTrue(model.IsBrushActive);

        model.UseBrush();

        Assert.AreEqual(1, model.BrushCount);
        Assert.IsFalse(model.IsBrushActive);
    }

    [Test]
    public void UseBrushWithZeroCount()
    {
        var model = new BonusPipeModel(0, 0);
        model.ToggleBrush();
        Assert.IsFalse(model.IsBrushActive);

        model.UseBrush();

        Assert.AreEqual(-1, model.BrushCount);
    }

    [Test]
    public void ToggleBrushWithPositiveCount()
    {
        var model = new BonusPipeModel(1, 0);
        model.ToggleBrush();
        Assert.IsTrue(model.IsBrushActive);
    }

    [Test]
    public void ToggleBrushWithZeroCount()
    {
        var model = new BonusPipeModel(0, 0);
        model.ToggleBrush();
        Assert.IsFalse(model.IsBrushActive);
    }

    [Test]
    public void UseHammerWithPositiveCount()
    {
        var model = new BonusPipeModel(0, 1);
        model.ToggleHammer();
        Assert.IsTrue(model.IsHammerActive);

        model.UseHammer();

        Assert.AreEqual(0, model.HammerCount);
        Assert.IsFalse(model.IsHammerActive);
    }

    [Test]
    public void UseHammerWithZeroCount()
    {
        var model = new BonusPipeModel(0, 0);
        model.ToggleHammer();
        Assert.IsFalse(model.IsHammerActive);

        model.UseHammer();

        Assert.AreEqual(-1, model.HammerCount);
    }

    [Test]
    public void ToggleHammerWithPositiveCount()
    {
        var model = new BonusPipeModel(0, 2);
        Assert.IsFalse(model.IsHammerActive);

        model.ToggleHammer();

        Assert.IsTrue(model.IsHammerActive);
    }

    [Test]
    public void ToggleHammerWithZeroCount()
    {
        var model = new BonusPipeModel(0, 0);
        model.ToggleHammer();

        Assert.IsFalse(model.IsHammerActive);
    }
}