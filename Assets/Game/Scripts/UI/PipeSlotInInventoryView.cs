using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PipeSlotInInventoryView : MonoBehaviour
{
    public Button Button => _button;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    [SerializeField] private PipeType _type;
    [SerializeField] private Image _pipeSkin;

    public void UpdateText(int count)
    {
        _text.text = count.ToString(); //выставляем в кружочек
        _button.interactable = count > 0; 
    }

    public void ChangePipeSkin(Sprite sprite)
    {
        _pipeSkin.sprite = sprite;
    }
}
