using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<Player>().Win();
        FindObjectOfType<SceneLoader>().LoadNextScene(levelLoadDelay);
    }
}
