using UnityEngine;

public class BonusPipeModel
{
    public int BrushCount { get; private set; }
    public int HammerCount { get; private set; }

    public bool IsBrushActive { get; private set; }
    public bool IsHammerActive { get; private set; }

    public bool IsBonusActive => IsBrushActive || IsHammerActive;

    private InventoryModel _inventory;

    public BonusPipeModel(int brushCount, int hammerCount, InventoryModel inventory)
    {
        BrushCount = brushCount;
        HammerCount = hammerCount;
        _inventory = inventory;
    }

    public void ToggleBrush()
    {
        if (BrushCount <= 0) return;

        IsBrushActive = !IsBrushActive;
        IsHammerActive = false;
    }

    public void ToggleHammer()
    {
        if (HammerCount <= 0) return;

        IsHammerActive = !IsHammerActive;
        IsBrushActive = false;
    }

    public void UseBrush()
    {
        BrushCount--;
        Deactivate();
    }

    public void UseHammer(PipeType type)
    {
        HammerCount--;
        _inventory.Delete(type);
        Deactivate();
    }

    public void Deactivate()
    {
        IsBrushActive = false;
        IsHammerActive = false;
    }
}
