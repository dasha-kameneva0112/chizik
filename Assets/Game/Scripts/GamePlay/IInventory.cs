using System;
using System.Collections.Generic;

public interface IInventory
{
    event Action<PipeType, int> PipeCountChanged;
    Dictionary<PipeType, int> PipeSegments { get; }

    bool IsEnough(PipeType pipe);
    void Use(PipeType pipe);
    void Delete(PipeType pipe);
}