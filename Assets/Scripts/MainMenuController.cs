using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen = default;
    [SerializeField] GameObject mainMenu = default;
    [SerializeField] Slider musicSlider = default;
    [SerializeField] float defaultMusic = 0.5f;
    [SerializeField] Slider difficultySlider = default;
    [SerializeField] float defaultDifficulty = 1f;

    private void Start()
    {
        optionsScreen.SetActive(false);
        musicSlider.value = PlayerPrefsController.GetMusicVolume();
        difficultySlider.value = PlayerPrefsController.GetDifficultyLevel();
    }

    private void Update()
    {
        var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(musicSlider.value);
        }
        else
        {
            Debug.LogWarning("Music player not found. Have you started from Main Menu?");
        }
    }

    public void LoadOptionsScreen()
    {
        mainMenu.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void BackToMainMenu()
    {
        PlayerPrefsController.SetMusicVolume(musicSlider.value);
        PlayerPrefsController.SetDifficultyLevel(difficultySlider.value);
        mainMenu.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void SetDefaults()
    {
        musicSlider.value = defaultMusic;
        difficultySlider.value = defaultDifficulty;
    }
}
