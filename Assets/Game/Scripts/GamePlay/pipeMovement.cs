using UnityEngine;
using UnityEngine.InputSystem;

public class PipeMovement : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer _spriteRenderer;
    private BonusApplyPipe _applyPipe;

    void Start()
    {
        mainCamera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _applyPipe = GetComponent<BonusApplyPipe>();
    }

    private void Update()
    {
        Vector2 worldPos = GetMouseWorldPosition(); 

        Rotate(worldPos, _applyPipe);

        if (_spriteRenderer.bounds.Contains(worldPos) && Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (AnyLevel.Instance.RemoveCount > 0)
            {
                if (_applyPipe.PipeType != PipeType.FinishPipe)
                {
                    Destroy(gameObject);
                    AnyLevel.Instance.UpdateRemoveCount();
                    InventoryModel.Instance.Delete(_applyPipe.PipeType);
                }
            }
        }
    }

    private void Rotate(Vector2 worldPos, BonusApplyPipe segment)
    {
        if (_spriteRenderer.bounds.Contains(worldPos) && Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (segment.PipeType != PipeType.FinishPipe)
            {
                transform.Rotate(0, 0, 90);
                return;
            }
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        if (Mouse.current == null) return Vector2.zero;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        return worldPos;
    }
}