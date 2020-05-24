using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        var waterRise = FindObjectOfType<VerticalScroll>();
        if (waterRise)
        {
            waterRise.isActive = false;
        }
        FindObjectOfType<Player>().Win();
        yield return new WaitForSeconds(levelLoadDelay);
        FindObjectOfType<SceneLoader>().LoadNextScene(1f);
    }
}
