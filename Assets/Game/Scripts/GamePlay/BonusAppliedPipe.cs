using UnityEngine;
using UnityEngine.InputSystem;

public class BonusAppliedPipe : MonoBehaviour
{
    [Header("Тип трубы")]
    [SerializeField] private PipeType _pipeType;

    [HideInInspector] public bool isPlayerPlaced = false;

    public PipeType GetPipeType() => _pipeType;

    private void Update()
    {
        if (Mouse.current == null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (BonusManager.Instance != null && BonusManager.Instance.IsBonusActive())
            {
                BonusManager.Instance.OnPipeClicked(this);
            }
        }
    }

    public void SetAsPlayerPlaced()
    {
        isPlayerPlaced = true;
    }


    public bool IsDirty()
    {
        return _pipeType == PipeType.DirtyStraightPipe ||
               _pipeType == PipeType.DirtyCornerPipe;
    }

}