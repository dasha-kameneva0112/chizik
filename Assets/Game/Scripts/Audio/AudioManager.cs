using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    public static AudioManager Instance => _instance;

    private static AudioManager _instance;

    public AudioManager()
    {
        _instance = this;
    }

    public void PlaySfx(AudioSource audio)
    {
        if (audio != null)
            audio.Play();
    }
}
