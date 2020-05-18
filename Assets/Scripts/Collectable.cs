using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int scoreValue = 1;

    bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp)
        {
            pickedUp = true;
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
