using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioChannel _audioChannel;
    [SerializeField] private Sprite _turnOnImage;
    [SerializeField] private Sprite _turnOffImage;

    private Image _image;

    void Start()
    {
        _image = _button.GetComponentInChildren<Image>();
    }

    public void ChangeImage(bool isOn)
    {
        if(isOn)
        {
            _image.sprite = _turnOnImage;
        }
        else
        {
            _image.sprite = _turnOffImage;
        }
    }
}