using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        PlayerPrefsController.SetMusicVolume(0.5f);
        PlayerPrefsController.SetDifficultyLevel(1f);
        audioSource.volume = PlayerPrefsController.GetMusicVolume();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
