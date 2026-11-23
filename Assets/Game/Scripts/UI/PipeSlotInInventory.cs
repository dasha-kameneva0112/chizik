using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PipeSlotInInventory : MonoBehaviour
{
    public Button Button => _button;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    [SerializeField] private PipeType _type;
    [SerializeField] private Image _pipeSkin;

    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.CountPipeChange += UpdateText;
    }

    void UpdateText(PipeType type, int count)
    {
        if (type == _type)
        {
            _text.text = count.ToString(); //выставляем в кружочек
            if (count == 0)
            {
                Disable();
            }
            if (count == 1 && _button.interactable == false)
            {
                _button.interactable = true;
            }
        }
    }

    public void Enable()
    {
        _button.interactable = true;

        if (_inventory.PipeSegments.TryGetValue(_type, out var count))
        {
            _text.text = count.ToString();
            if (count == 0)
            {
                Disable();
            }
        }
    }

    public void Disable()
    {
        _button.interactable = false;
    }

    public void ChangePipeSkin(Sprite sprite)
    {
        _pipeSkin.sprite = sprite;
    }
}
