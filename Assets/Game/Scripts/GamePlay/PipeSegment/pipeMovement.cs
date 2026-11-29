using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class pipeMovement : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            Collider2D[] allColliders = GetComponents<Collider2D>();

            foreach (Collider2D col in allColliders)
            {
                if (col.bounds.Contains(worldPos))
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            Collider2D[] allColliders = GetComponents<Collider2D>();

            Collider2D hit = Physics2D.OverlapPoint(worldPos);

            foreach (Collider2D col in allColliders)
            {
                BonusAppliedPipe segment = hit.GetComponent<BonusAppliedPipe>();

                if (col.bounds.Contains(worldPos) && AnyLevel.instance.removeCount > 0)
                {
                    Destroy(gameObject);
                    AnyLevel.instance.UpdateRemoveCount();
                    PipeInInventory.Instance.Delete(segment.GetPipeType());
                }
            }
        }
                
    }
}
