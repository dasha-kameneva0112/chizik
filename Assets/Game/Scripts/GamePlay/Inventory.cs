using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<PipeType, int> CountPipeChange;

    public static Inventory Instance => _instance;

    public Dictionary<PipeType, int> PipeSegments => _pipeSegments;

    [Header("Number of segment types")]
    [SerializeField] private int _countStraightPipe;
    [SerializeField] private int _countCornerPipe;
    [SerializeField] private int _countTriangularPipe;
    [SerializeField] private int _countCruciformPipe;

    private static Inventory _instance;
    private readonly Dictionary<PipeType, int> _pipeSegments = new Dictionary<PipeType, int>();

    private void Awake()
    {
        _instance = this;

        _pipeSegments[PipeType.StraightPipe] = _countStraightPipe;
        _pipeSegments[PipeType.CornerPipe] = _countCornerPipe;
        _pipeSegments[PipeType.TriangularPipe] = _countTriangularPipe;
        _pipeSegments[PipeType.CruciformPipe] = _countCruciformPipe;
    }

    public void Use(PipeType pipe)
    {
        if (!_pipeSegments.ContainsKey(pipe))
        {
            throw new KeyNotFoundException();
        }

        _pipeSegments[pipe]--;
        CountPipeChange?.Invoke(pipe, _pipeSegments[pipe]);
    }

    public bool IsEnough(PipeType pipe)
    {
        if (!_pipeSegments.ContainsKey(pipe))
        {
            throw new KeyNotFoundException();
        }
        return _pipeSegments[pipe] > 0;
    }

    public void Delete(PipeType pipe) 
    {
        if (!_pipeSegments.ContainsKey(pipe))
        {
            throw new KeyNotFoundException();
        }
        _pipeSegments[pipe]++;
        CountPipeChange?.Invoke(pipe, _pipeSegments[pipe]);
    }
}

public enum PipeType
{
    StraightPipe,
    CornerPipe,
    TriangularPipe,
    CruciformPipe,

    DirtyStraightPipe,
    DirtyCornerPipe
}