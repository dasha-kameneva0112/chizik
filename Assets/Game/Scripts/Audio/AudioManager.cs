using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => _instance;

    [Header("Sources")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    [Header("Sound clips")]
    [SerializeField] private AudioClip _sfxVictory;
    [SerializeField] private AudioClip _sfxLoose;
    [SerializeField] private AudioClip _sfxSponge;
    [SerializeField] private AudioClip _sfxHammer;
    [SerializeField] private AudioClip _sfxFreeDelete;

    [SerializeField] private AudioClip _menuMusic;

    private static AudioManager _instance;
    private bool _isSfxOn = true;
    private bool _isMusicOn = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        PlayMusic(_menuMusic);
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
    }

    public void ClickOnMusic()
    {
        _isMusicOn = !_isMusicOn;
        if (_isMusicOn)
            PlayMusic(_menuMusic);
        else
            StopMusic();
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

    public void DeletePipe()
    {
        PlaySfx(_sfxFreeDelete);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        if (_musicSource.clip == clip && _musicSource.isPlaying)
            return;

        _musicSource.clip = clip;

        if (_isMusicOn)
            _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
