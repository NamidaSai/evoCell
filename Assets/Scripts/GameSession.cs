using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] float playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText = default;
    [SerializeField] TextMeshProUGUI scoreText = default;
    [SerializeField] GameObject loseLabel = default;

    [SerializeField] public bool noDeathMode = false;
    [SerializeField] bool noLivesMode = false;

    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if(gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetLivesOnDifficulty();
        scoreText.text = score.ToString();
        loseLabel.SetActive(false);
    }

    private void SetLivesOnDifficulty()
    {
        var difficulty = PlayerPrefsController.GetDifficultyLevel();
        if (difficulty == 2f)
        {
            playerLives = 1f;
            livesText.text = playerLives.ToString();
        }
        else if (difficulty == 1f)
        {
            playerLives = 3f;
            livesText.text = playerLives.ToString();
        }
        else if (difficulty == 0f)
        {
            noLivesMode = true;
            livesText.text = "∞";
        }
        else
        {
            Debug.LogError("Invalid Difficulty Level.");
        }
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath(float delayInSeconds)
    {
        var sceneLoader = FindObjectOfType<SceneLoader>();
        var sfxPlayer = FindObjectOfType<SFXPlayer>();

        if (playerLives > 1)
        {
            if (!noLivesMode) { TakeLife(); }
            AudioSource.PlayClipAtPoint(sfxPlayer.GetLifeLostClip(), Camera.main.transform.position, sfxPlayer.GetLevelVolume());
            sceneLoader.RestartLevel(delayInSeconds);
        }
        else
        {
            if (!noLivesMode) { TakeLife(); }
            AudioSource.PlayClipAtPoint(sfxPlayer.GetLoseGameClip(), Camera.main.transform.position, sfxPlayer.GetGameSessionVolume());
            HandleLoseCondition();
        }
    }

    private void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
    }
}
