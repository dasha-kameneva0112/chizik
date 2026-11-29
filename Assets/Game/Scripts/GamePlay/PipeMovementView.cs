using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PipeMovementView : MonoBehaviour
{
    public event Action<Vector2> MouseMoved;
    public event Action<Vector2> RightClick;
    public event Action<Vector2> RemovePressed;

    private Camera mainCamera;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        mainCamera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 worldPos = GetMouseWorldPosition();

        MouseMoved?.Invoke(worldPos);

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            RightClick?.Invoke(worldPos);
        }

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            RemovePressed?.Invoke(worldPos);
        }
    }

    public bool Contains(Vector2 worldPos)
    {
        return _spriteRenderer.bounds.Contains(worldPos);
    }

    public void RotatePipe()
    {
        transform.Rotate(0, 0, 90);
    }

    public void DestroyPipe()
    {
        Destroy(gameObject);
    }

    private Vector2 GetMouseWorldPosition()
    {
        if (Mouse.current == null) return Vector2.zero;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        return worldPos;
    }
}
