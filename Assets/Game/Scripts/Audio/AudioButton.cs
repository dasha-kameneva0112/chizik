using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioChannel _audioChannel;
    [SerializeField] private Sprite _turnOnImage;
    [SerializeField] private Sprite _turnOffImage;

    private Image _image;
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = AudioManager.Instance;
        _image = _button.GetComponentInChildren<Image>();
        
        _button.onClick.AddListener(OnClick);
        ChangeImage(_audioManager.GetButtonOn(_audioChannel));
    }

    private void OnClick()
    {
        if(_audioChannel == AudioChannel.Music)
        {
            _audioManager.ClickOnMusic();
        }
        else
        {
            _audioManager.ClickOnSfx();
        }
        ChangeImage(_audioManager.GetButtonOn(_audioChannel));
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