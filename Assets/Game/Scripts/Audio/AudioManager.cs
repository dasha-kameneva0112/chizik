using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => _instance;

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private Button _sfxButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private AudioClip _sfxVictory;
    [SerializeField] private AudioClip _sfxLoose;
    [SerializeField] private AudioClip _sfxSponge;
    [SerializeField] private AudioClip _sfxHammer;

    private static AudioManager _instance;
    private AudioButton _audioButtonSfx;
    private AudioButton _audioButtonMusic;
    private bool _isSfxOn;
    private bool _isMusicOn;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _isSfxOn = true;
            _isMusicOn = true;
        }
    }

    private void Start()
    {
        _sfxButton.onClick.AddListener(ClickOnSfx);
        _musicButton.onClick.AddListener(ClickOnMusic);
        _audioButtonSfx = _sfxButton.GetComponent<AudioButton>();
        _audioButtonMusic = _musicButton.GetComponent<AudioButton>();

        ChangeImageOnButtons();
    }

    public void ChangeImageOnButtons()
    {
        _audioButtonSfx.ChangeImage(_isSfxOn);
        _audioButtonMusic.ChangeImage(_isMusicOn);
    }

    public bool GetButtonOn(AudioChannel audioChannel)
    {
        if (audioChannel == AudioChannel.Sfx)
            return _isSfxOn;
        if(audioChannel == AudioChannel.Music)
            return _isMusicOn;
        return false;
    }

    public void PlaySfx(AudioClip audio)
    {
        PlaySfx(audio, 1);
    }

    public void PlaySfx(AudioClip audio, float volume)
    {
        if (audio != null && _isSfxOn)
        {
            //audio.Play();
            _sfxSource.PlayOneShot(audio, volume);
        }
    }

    public void StopClickSound()
    {
        if (_sfxSource != null)
            _sfxSource.Stop();
    }

    public void ClickOnSfx()
    {
        _isSfxOn = !_isSfxOn;
        _audioButtonSfx.ChangeImage(_isSfxOn);
    }

    public void ClickOnMusic()
    {
        _isMusicOn = !_isMusicOn;
        _audioButtonMusic.ChangeImage(_isMusicOn);
    }

    public void TurnOnSfxWin()
    {
        PlaySfx(_sfxVictory);
    }

    public void TurnOnSfxLoose()
    {
        PlaySfx(_sfxLoose);
    }

    public void TurnOnSfxSponge()
    {
        PlaySfx(_sfxSponge);
    }

    public void TurnOnSfxHammer()
    {
        PlaySfx(_sfxHammer);
    }
}
