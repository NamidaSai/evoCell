using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject transition = default;
    [SerializeField] float transitionDuration = 1f;

    int currentSceneIndex;

    private void Start()
    {
        transition.SetActive(true);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(WaitAndStart(transitionDuration));
    }

    IEnumerator WaitAndStart(float delayInSeconds)
    {
        Time.timeScale = 1f;
        transition.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(0);
    }
    public void LoadNextScene(float delayInSeconds)
    {
        StartCoroutine(WaitAndLoad(delayInSeconds));
    }

    IEnumerator WaitAndLoad(float delayInSeconds)
    {
        transition.GetComponent<Animator>().SetTrigger("FadeIn");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(currentSceneIndex + 1);
        transition.GetComponent<Animator>().SetTrigger("FadeOut");
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

