using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip _soundClip;
    //private AudioSource _audioSource;
    private Button _button;
    private AudioManager _audioManager;

    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
        _audioManager = AudioManager.Instance;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        _audioManager.PlaySfx(_soundClip);
    }
}
