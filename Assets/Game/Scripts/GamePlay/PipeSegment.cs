using System;
using UnityEngine;

public class PipeSegment : MonoBehaviour
{
    [Header("Тип трубы")]
    [SerializeField] private PipeType _pipeType;

    [HideInInspector] public bool isPlayerPlaced = false;

    [SerializeField] private SpriteRenderer spriteRenderer;

    
    [Header("Спрайты труб")]
    [SerializeField] private Sprite cleanSprite;   
    [SerializeField] private Sprite dirtySprite;   

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateVisual(); 
    }

    
    public void ClearDirty()
    {
        if (!IsDirty()) return;

        if (_pipeType == PipeType.DirtyStraightPipe)
            _pipeType = PipeType.StraightPipe;
        else if (_pipeType == PipeType.DirtyCornerPipe)
            _pipeType = PipeType.CornerPipe;

        spriteRenderer.sprite = cleanSprite;
        Debug.Log("Труба очищена!");
    }

    public bool IsDirty()
    {
        return _pipeType == PipeType.DirtyStraightPipe ||
               _pipeType == PipeType.DirtyCornerPipe;
    }

    private void UpdateVisual()
    {
        if (spriteRenderer == null) return;

        if (IsDirty() && dirtySprite != null)
            spriteRenderer.sprite = dirtySprite;
        else if (cleanSprite != null)
            spriteRenderer.sprite = cleanSprite;
    }

    // Клик для бонусов
    private void OnMouseDown()
    {
        if (BonusManager.Instance != null && BonusManager.Instance.IsBonusActive())
        {
            BonusManager.Instance.OnPipeClicked(this);
        }
    }

    public void SetAsPlayerPlaced()
    {
        isPlayerPlaced = true;
    }
}