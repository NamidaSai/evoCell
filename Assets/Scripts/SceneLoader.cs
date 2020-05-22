using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene(float delayInSeconds)
    {
        StartCoroutine(WaitAndLoad(delayInSeconds));
    }

    IEnumerator WaitAndLoad(float delayInSeconds)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartLevel(float delayInSeconds)
    {
        StartCoroutine(WaitAndReload(delayInSeconds));
    }

    IEnumerator WaitAndReload(float delayInSeconds)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit.");
    }
}
