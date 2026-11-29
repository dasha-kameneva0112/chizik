using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PipeMovement : MonoBehaviour
{
    private PipeMovementPresenter _presenter;

    private void Awake()
    {
        var view = GetComponent<PipeMovementView>();
        _presenter = new PipeMovementPresenter(view);
    }
}
