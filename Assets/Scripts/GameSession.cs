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
        playerLives -= PlayerPrefsController.GetDifficultyLevel();
        scoreText.text = score.ToString();
        livesText.text = playerLives.ToString();
        loseLabel.SetActive(false);
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath(float delayInSeconds, bool noLivesMode)
    {
        var sceneLoader = FindObjectOfType<SceneLoader>();
        if(noLivesMode) { sceneLoader.RestartLevel(delayInSeconds); return; }
        if (playerLives > 1)
        {
            TakeLife();
            sceneLoader.RestartLevel(delayInSeconds);
        }
        else
        {
            TakeLife();
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
