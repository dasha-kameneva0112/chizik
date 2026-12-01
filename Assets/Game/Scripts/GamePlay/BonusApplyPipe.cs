using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BonusApplyPipe : MonoBehaviour, IBonusPipeView
{
    [Header("Тип трубы")]
    [SerializeField] private PipeType _pipeType;

    private BonusPipePresenter _presenter;

    public event Action OnClicked;

    public BonusPipePresenter Presenter => _presenter;

    public void Awake()
    {
        _presenter = new BonusPipePresenter(_pipeType, this);
    }

    private void Update()
    {
        if (Mouse.current == null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            OnClicked?.Invoke();
        }
    }


    public Vector3  Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    public void DestroyPipe () => Destroy(gameObject);
}