using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f,2f);
    [SerializeField] float deathFXTime = 1f;

    bool isAlive = true;

    Animator myAnimator;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;
    GameSession gameSession;
    AudioSource myAudioSource;
    SFXPlayer sfxPlayer;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        myAudioSource = GetComponent<AudioSource>();
        sfxPlayer = FindObjectOfType<SFXPlayer>();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Walk();
        Jump();
        Fall();
        ClimbLadder();
        FlipSprite();
        if (gameSession)
        {
            Die();
        }
        Test(); // for debugging purposes only, triggered with Tab key
    }

    private void Walk()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 walkVelocity = new Vector2(controlThrow * walkSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = walkVelocity;

        bool playerHasHorizontalSpeed = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        myAnimator.SetBool("isWalking", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1f, 1f);
        }
    }

    private void Jump()
    {
        bool playerIsTouchingGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (!playerIsTouchingGround) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
            myAnimator.SetTrigger("Jumped");
            myAudioSource.PlayOneShot(sfxPlayer.GetJumpClip(),sfxPlayer.GetPlayerVolume());
        }
    }

    private void Fall()
    {
        bool playerHasDownwardSpeed = (myRigidbody.velocity.y < Mathf.Epsilon);
        bool playerIsTouchingGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool playerIsTouchingLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladders"));

        myAnimator.SetBool("isFalling", playerHasDownwardSpeed && !playerIsTouchingGround && !playerIsTouchingLadder);
    }

    private void ClimbLadder()
    {
        bool playerIsTouchingLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladders"));

        if (!playerIsTouchingLadder)
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = (Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon);
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            if(gameSession.noDeathMode) { Debug.Log("Player is dead."); return; }
            if (!isAlive) { return; }
            isAlive = false;
            ResetStates();
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            gameSession.ProcessPlayerDeath(deathFXTime);
        }
    }

    public void Win()
    {
        isAlive = false;
        ResetStates();
        myAudioSource.PlayOneShot(sfxPlayer.GetWinLevelClip(), sfxPlayer.GetLevelVolume());
    }

    public void PlayFootstepSFX()
    {
        bool playerIsTouchingGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!playerIsTouchingGround) { return; }
        myAudioSource.PlayOneShot(sfxPlayer.GetFootstepsClip(), sfxPlayer.GetPlayerVolume());
    }

    private void ResetStates()
    {
        myAnimator.SetBool("isWalking", false);
        myAnimator.SetBool("isFalling", false);
        myAnimator.SetBool("isClimbing", false);
        myRigidbody.gravityScale = gravityScaleAtStart;
        myRigidbody.velocity = new Vector2(0f, 0f);
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //insert function to test
            FindObjectOfType<SceneLoader>().LoadNextScene(0f);
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            if(gameSession.noDeathMode)
            {
                gameSession.noDeathMode = false;
            }
            else if (!gameSession.noDeathMode)
            {
                gameSession.noDeathMode = true;
            }
        }
    }
}
