using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class pipeMovement : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) { }

    public void OnPointerDown(PointerEventData eventData) { }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            rightClick();
        }

        if (Keyboard.current != null && Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            backSpaceClick();
        }
    }

    void rightClick()
    {
        transform.Rotate(0, 0, 90, Space.Self);
    }

    void backSpaceClick()
    {
        Destroy(gameObject);
    }
}
