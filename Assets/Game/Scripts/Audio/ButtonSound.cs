using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private AudioSource _audioSource;
    private Button _button;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        AudioManager.Instance.PlaySfx(_audioSource);
    }
}
