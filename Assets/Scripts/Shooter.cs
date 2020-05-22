using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject discPrefab = default;
    [SerializeField] GameObject gun = default;
    [SerializeField] float projectileSpeedX = 0f;
    [SerializeField] float projectileSpeedY = 10f;

    GameObject projectileParent;
    AudioSource myAudioSource;
    SFXPlayer sfxPlayer;

    const string PROJECTILE_PARENT_NAME = "Projectiles";

    private void Start()
    {
        CreateProjectileParent();
        sfxPlayer = FindObjectOfType<SFXPlayer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if(!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    public void Fire()
    {
        GameObject disc = Instantiate
                          (discPrefab, gun.transform.position, Quaternion.identity)
                          as GameObject;
        disc.transform.parent = projectileParent.transform;
        disc.GetComponent<Rigidbody2D>().velocity = new Vector2
                                               (projectileSpeedX,
                                                projectileSpeedY);
        myAudioSource.PlayOneShot(sfxPlayer.GetShooterClip(), sfxPlayer.GetEnemyVolume());
    }
}
