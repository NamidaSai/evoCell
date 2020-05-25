using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] Sprite[] hitSprites = default;
    [SerializeField] GameObject particlesPrefab = default;

    int timesHit = 0;

    SFXPlayer sfxPlayer;

    private void Start()
    {
        sfxPlayer = FindObjectOfType<SFXPlayer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit();
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;

        if(timesHit >= maxHits)
        {
            DestroyBlock();
            AudioSource.PlayClipAtPoint(sfxPlayer.GetBlockBreakClip(), Camera.main.transform.position, sfxPlayer.GetBlockVolume());
            GameObject explosion = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
            Destroy(explosion,2f);
        }
        else
        {
            ShowNextHitSprite();
            AudioSource.PlayClipAtPoint(sfxPlayer.GetBlockHitClip(), Camera.main.transform.position, sfxPlayer.GetBlockVolume());
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;

        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Sprite missing from Hit Sprites. Add Sprite to array.");
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }
}
