using UnityEngine;
using UnityEngine.InputSystem;

public class PipeMovement : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 worldPos = GetMouseWorldPosition();

        Rotate(worldPos);

        if (_spriteRenderer.bounds.Contains(worldPos) && Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (AnyLevel.Instance.RemoveCount > 0)
            {
                BonusApplyPipe segment = GetComponent<BonusApplyPipe>();

                Destroy(gameObject);
                AnyLevel.Instance.UpdateRemoveCount();
                InventoryModel.Instance.Delete(segment.Presenter.GetPipeType());
            }
        }
    }

    private void Rotate(Vector2 worldPos)
    {
        if (_spriteRenderer.bounds.Contains(worldPos) && Mouse.current.rightButton.wasPressedThisFrame)
        {
            transform.Rotate(0, 0, 90);
            return;
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