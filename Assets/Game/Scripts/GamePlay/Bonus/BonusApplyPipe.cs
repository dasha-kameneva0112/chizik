using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BonusApplyPipe : MonoBehaviour, IPipeView
{
    [SerializeField] private PipeType pipeType;

    public PipeType PipeType => pipeType;
    public bool IsPlayerPlaced { get; private set; } = false;
    public bool IsDirty =>
        pipeType == PipeType.DirtyStraightPipe ||
        pipeType == PipeType.DirtyCornerPipe;

    public event Action<IPipeView> ClickedPipe;

    public bool IsFinishPipe { get; private set; }

    public void SetAsPlayerPlaced()
    {
        IsPlayerPlaced = true;
    }

    public void DestroyPipe()
    {
        Destroy(gameObject);
    }

    public void SetFinishPipe()
    {
        IsFinishPipe = true;
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            ClickedPipe?.Invoke(this);
        }
    }
}
