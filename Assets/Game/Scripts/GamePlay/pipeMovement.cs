using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class pipeMovement : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer _sr;

    void Start()
    {
        mainCamera = Camera.main;
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (_sr.bounds.Contains(worldPos) && Mouse.current.rightButton.wasPressedThisFrame)
        {
            transform.Rotate(0, 0, 90);
            return;
        }

        if (_sr.bounds.Contains(worldPos) && Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (AnyLevel.instance.removeCount > 0)
            {
                    BonusAppliedPipe segment = GetComponent<BonusAppliedPipe>();

                    Destroy(gameObject);
                    AnyLevel.instance.UpdateRemoveCount();
                    Inventory.Instance.Delete(segment.GetPipeType());
            }
        }
                
    }
}
