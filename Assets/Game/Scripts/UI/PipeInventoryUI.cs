using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PipeInventoryUI : MonoBehaviour
{
    [SerializeField] private PipeSlotInInventory _straightPipe;
    [SerializeField] private PipeSlotInInventory _cornerPipe;
    [SerializeField] private PipeSlotInInventory _triangularPipe;
    [SerializeField] private PipeSlotInInventory _cruciformPipe;

    [Header("Pipe Prefabs")]
    [SerializeField] private Pipe _straightPipePrefab;
    [SerializeField] private Pipe _cornerPipePrefab;
    [SerializeField] private Pipe _triangularPipePrefab;
    [SerializeField] private Pipe _cruciformPipePrefab;

    [SerializeField] private GameField _gameField;

    void Start()
    {
        _straightPipe.Button.onClick.AddListener(OnStraightPipeCLicked);
        _cornerPipe.Button.onClick.AddListener(OnCornerPipeClicked);
        _triangularPipe.Button.onClick.AddListener(OnTriangularPipeClicked);
        _cruciformPipe.Button.onClick.AddListener(OnCruciformPipeClicked);

        _straightPipe.Enable();
        _cornerPipe.Enable();
        _triangularPipe.Enable();
        _cruciformPipe.Enable();

        //GameSettings.Instance.SkinChanged += ChangeSkin;
        ChangeSkin();
    }

    private void ChangeSkin()
    {
        SkinData pipe = GameSettings.Instance.CurrentPipeSkin;

        _straightPipePrefab.SetSkin(pipe.icon);
        _triangularPipePrefab.SetSkin(pipe.triangularPipe);
        _cornerPipePrefab.SetSkin(pipe.cornerPipe);
        _cruciformPipePrefab.SetSkin(pipe.cruciformPipe);

        _straightPipe.ChangePipeSkin(pipe.icon);
        _cornerPipe.ChangePipeSkin(pipe.cornerPipe);
        _triangularPipe.ChangePipeSkin(pipe.triangularPipe);
        _cruciformPipe.ChangePipeSkin(pipe.cruciformPipe);

        // Обновляем ВСЕ существующие трубы на сцене
        //Pipe[] allPipes = FindObjectsOfType<Pipe>();
        //foreach (Pipe p in allPipes)
        //{
        //    p.SetSkin(pipe.cornerPipe);
        //}
    }

    public void OnStraightPipeCLicked()
    {
        if (Inventory.Instance.IsEnough(PipeType.StraightPipe))
        {
            Inventory.Instance.Use(PipeType.StraightPipe);
            _gameField.StartPlacingPipe(_straightPipePrefab);
        }
    }

    public void OnCornerPipeClicked()
    {
        if (Inventory.Instance.IsEnough(PipeType.CornerPipe))
        {
            Inventory.Instance.Use(PipeType.CornerPipe);
            _gameField.StartPlacingPipe(_cornerPipePrefab);
        }
    }

    public void OnTriangularPipeClicked()
    {
        if (Inventory.Instance.IsEnough(PipeType.TriangularPipe))
        {
            Inventory.Instance.Use(PipeType.TriangularPipe);
            _gameField.StartPlacingPipe(_triangularPipePrefab);
        }
    }

    public void OnCruciformPipeClicked()
    {
        if (Inventory.Instance.IsEnough(PipeType.CruciformPipe))
        {
            Inventory.Instance.Use(PipeType.CruciformPipe);
            _gameField.StartPlacingPipe(_cruciformPipePrefab);
        }
    }
}