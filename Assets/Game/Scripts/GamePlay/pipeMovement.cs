using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PipeMovement : MonoBehaviour
{
<<<<<<< HEAD
    private Camera mainCamera;
    private SpriteRenderer _spriteRenderer;
    private AnyLevel _anyLevel;
    private Inventory _inventory;
    private BonusPipePresenter _pipePresenter;
    private BonusApplyPipe _pipeView;
=======
    private PipeMovementPresenter _presenter;
>>>>>>> eee20564c418043860b2134aa07d31d2d73d44e4

    private void Awake()
    {
<<<<<<< HEAD
        mainCamera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anyLevel = AnyLevel.Instance;
        _inventory = Inventory.Instance;
        _pipeView = GetComponent<BonusApplyPipe>();
    }

    private void Update()
    {
        Vector2 worldPos = GetMouseWorldPosition();

        Rotate(worldPos);

        if (_spriteRenderer.bounds.Contains(worldPos) && Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (_anyLevel.removeCount > 0)
            {
                PipeType pipeType = _pipePresenter.GetPipeType();

                Destroy(gameObject);
                _anyLevel.UpdateRemoveCount();
                _inventory.Delete(pipeType);
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
=======
        var view = GetComponent<PipeMovementView>();
        _presenter = new PipeMovementPresenter(view);
>>>>>>> eee20564c418043860b2134aa07d31d2d73d44e4
    }
}
