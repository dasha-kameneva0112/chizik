using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InventoryModelTests : MonoBehaviour
{
    [Test]
    public void Test1_UseStraightPipe_DecreasesCountFrom3To2()
    {
        var initialInventory = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 3 },
            { PipeType.CornerPipe, 1 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 0 }
        };
        var inventory = new InventoryModel(initialInventory);

        inventory.Use(PipeType.StraightPipe);

        Assert.AreEqual(2, inventory.PipeSegments[PipeType.StraightPipe]);
    }

    [Test]
    public void Test2_UseCornerPipe_DecreasesCountFrom1To0()
    {
        var initialInventory = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 3 },
            { PipeType.CornerPipe, 1 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 0 }
        };
        var inventory = new InventoryModel(initialInventory);

        inventory.Use(PipeType.CornerPipe);

        Assert.AreEqual(0, inventory.PipeSegments[PipeType.CornerPipe]);
    }

    [Test]
    public void Test3_DeleteTriangularPipe_IncreasesCountFrom2To3()
    {
        var initialInventory = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 3 },
            { PipeType.CornerPipe, 1 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 0 }
        };
        var inventory = new InventoryModel(initialInventory);

        inventory.Delete(PipeType.TriangularPipe);

        Assert.AreEqual(3, inventory.PipeSegments[PipeType.TriangularPipe]);
    }

    [Test]
    public void Test4_DeleteCruciformPipe_IncreasesCountFrom0To1()
    {
        var initialInventory = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 3 },
            { PipeType.CornerPipe, 1 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 0 }
        };
        var inventory = new InventoryModel(initialInventory);

        inventory.Delete(PipeType.CruciformPipe);

        Assert.AreEqual(1, inventory.PipeSegments[PipeType.CruciformPipe]);
    }

    [Test]
    public void Test5_IsEnoughStraightPipeWith2_ReturnsTrue()
    {
        var initialInventory = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 2 },
            { PipeType.CornerPipe, 1 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 0 }
        };
        var inventory = new InventoryModel(initialInventory);

        bool result = inventory.IsEnough(PipeType.StraightPipe);

        Assert.IsTrue(result);
    }

    [Test]
    public void Test6_IsEnoughCornerPipeWith0_ReturnsFalse()
    {
        var initialInventory = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 3 },
            { PipeType.CornerPipe, 0 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 0 }
        };
        var inventory = new InventoryModel(initialInventory);

        bool result = inventory.IsEnough(PipeType.CornerPipe);

        Assert.IsFalse(result);
    }
}
