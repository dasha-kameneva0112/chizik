using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PipeMovement : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer _spriteRenderer;
    private AnyLevel _anyLevel;
    private Inventory _inventory;

    void Start()
    {
        mainCamera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anyLevel = AnyLevel.Instance;
        _inventory = Inventory.Instance;
    }

    private void Update()
    {
        Vector2 worldPos = GetMouseWorldPosition();

        Rotate(worldPos);

        if (_spriteRenderer.bounds.Contains(worldPos) && Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (_anyLevel.removeCount > 0)
            {
                BonusApplyPipe segment = GetComponent<BonusApplyPipe>();

                Destroy(gameObject);
                _anyLevel.UpdateRemoveCount();
                _inventory.Delete(segment.GetPipeType());
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
