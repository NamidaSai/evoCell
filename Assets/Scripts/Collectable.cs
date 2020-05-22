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
            var sfxPlayer = FindObjectOfType<SFXPlayer>();
            pickedUp = true;
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            AudioSource.PlayClipAtPoint(sfxPlayer.GetGemClip(), Camera.main.transform.position, sfxPlayer.GetInteractablesVolume());
            Destroy(gameObject);
        }
    }
}
