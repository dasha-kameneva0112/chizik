using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : IInventory
{
    public event Action<PipeType, int> PipeCountChanged;
    public static InventoryModel Instance => _instance;
    public Dictionary<PipeType, int> PipeSegments => _pipeSegments;

    private static InventoryModel _instance;
    private readonly Dictionary<PipeType, int> _pipeSegments = new Dictionary<PipeType, int>();

    public InventoryModel(Dictionary<PipeType, int> startCounts)
    {
        _instance = this;

        _pipeSegments = new Dictionary<PipeType, int>(startCounts);
    }

    public void Use(PipeType type)
    {
        if (!_pipeSegments.ContainsKey(type))
        {
            throw new KeyNotFoundException();
        }

        _pipeSegments[type]--;
        PipeCountChanged?.Invoke(type, _pipeSegments[type]);
    }

    public bool IsEnough(PipeType type)
    {
        if (!_pipeSegments.ContainsKey(type))
        {
            throw new KeyNotFoundException();
        }
        return _pipeSegments[type] > 0;
    }

    public void Delete(PipeType type)
    {
        if (!_pipeSegments.ContainsKey(type))
        {
            throw new KeyNotFoundException();
        }
        _pipeSegments[type]++;
        PipeCountChanged?.Invoke(type, _pipeSegments[type]);
    }
}
