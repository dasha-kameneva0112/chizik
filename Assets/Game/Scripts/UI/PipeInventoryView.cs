using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PipeInventoryView : MonoBehaviour
{
    public event Action OnStraightPipeCLicked;
    public event Action OnCornerPipeClicked;
    public event Action OnTriangularPipeClicked;
    public event Action OnCruciformPipeClicked;

    [SerializeField] private PipeSlotInInventoryView _straightPipe;
    [SerializeField] private PipeSlotInInventoryView _cornerPipe;
    [SerializeField] private PipeSlotInInventoryView _triangularPipe;
    [SerializeField] private PipeSlotInInventoryView _cruciformPipe;

    [Header("Pipe Prefabs")]
    [SerializeField] public Pipe _straightPipePrefab;
    [SerializeField] public Pipe _cornerPipePrefab;
    [SerializeField] public Pipe _triangularPipePrefab;
    [SerializeField] public Pipe _cruciformPipePrefab;

    void Start()
    {
        _straightPipe.Button.onClick.AddListener( () => OnStraightPipeCLicked?.Invoke() );
        _cornerPipe.Button.onClick.AddListener( () => OnCornerPipeClicked?.Invoke());
        _triangularPipe.Button.onClick.AddListener( () => OnTriangularPipeClicked?.Invoke() );
        _cruciformPipe.Button.onClick.AddListener( () => OnCruciformPipeClicked?.Invoke() );
    }

    public void UpdateCount(PipeType type, int count)
    {
        if (type == PipeType.StraightPipe)
            _straightPipe.UpdateText(count);
        if (type == PipeType.CornerPipe)
            _cornerPipe.UpdateText(count);
        if (type == PipeType.TriangularPipe)
            _triangularPipe.UpdateText(count);
        if (type == PipeType.CruciformPipe)
            _cruciformPipe.UpdateText(count);
    }

    public void ChangeSkin(SkinData skin)
    {
        _straightPipePrefab.SetSkin(skin.icon);
        _triangularPipePrefab.SetSkin(skin.triangularPipe);
        _cornerPipePrefab.SetSkin(skin.cornerPipe);
        _cruciformPipePrefab.SetSkin(skin.cruciformPipe);

        _straightPipe.ChangePipeSkin(skin.icon);
        _cornerPipe.ChangePipeSkin(skin.cornerPipe);
        _triangularPipe.ChangePipeSkin(skin.triangularPipe);
        _cruciformPipe.ChangePipeSkin(skin.cruciformPipe);

        // Обновляем ВСЕ существующие трубы на сцене
        //Pipe[] allPipes = FindObjectsOfType<Pipe>();
        //foreach (Pipe p in allPipes)
        //{
        //    p.SetSkin(pipe.cornerPipe);
        //}
    }

    public void HideInventory()
    {
        gameObject.SetActive(false);
    }

    public Pipe GetPipePrefab(PipeType type)
    {
        if (type == PipeType.StraightPipe)
            return _straightPipePrefab;
        if (type == PipeType.CornerPipe)
            return _cornerPipePrefab;
        if (type == PipeType.TriangularPipe)
            return _triangularPipePrefab;
        if (type == PipeType.CruciformPipe)
            return _cruciformPipePrefab;
        return null;
    }
}
