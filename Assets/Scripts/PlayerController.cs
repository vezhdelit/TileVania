using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float maxSpeed = 30f;
    [SerializeField] float secondJumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f,25f);

    //State
    bool isAlive = true;
    bool canJump = true;
    bool canDoubleJump = true;
    //Cached component referenced
    Rigidbody2D rb2D;
    Animator anim;
    CapsuleCollider2D bodyCollider2D;
    CircleCollider2D feetCollider2D;

    float gravityScaleAtStart;

    void Start()
    {
        Time.timeScale = 1f;
        
        //Get References to vriables
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<CircleCollider2D>();
        gravityScaleAtStart = rb2D.gravityScale;
    }

    void Update()
    {
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, maxSpeed);
        }
        //Stops all the player abilities
        if (!isAlive) { return; } 

        Run();
        Jump();
        ClimbLadder();
        Die();
        FlipSprite();

    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rb2D.velocity.y);
        rb2D.velocity = playerVelocity;

        bool isPlayerMoving = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Running", isPlayerMoving);

    }
    private void Jump()
    {
        if (!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            if(canDoubleJump && Input.GetButtonDown("Jump"))
                {
                    Vector2 jumpVelocityToAdd = new Vector2(0f, secondJumpSpeed);
                    rb2D.velocity = new Vector2(0,0);
                    rb2D.velocity += jumpVelocityToAdd;
                    canDoubleJump = false;
                }

            canJump = true;
            return; 
        }

        canDoubleJump = true;
        if (Input.GetButton("Jump") && canJump)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rb2D.velocity += jumpVelocityToAdd;
            canJump = false;
        }
    }
    private void ClimbLadder()
    {
        if (!bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            anim.SetBool("Climbing", false);
            rb2D.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rb2D.velocity.x, controlThrow * climbSpeed);
        rb2D.velocity = climbVelocity;

        rb2D.gravityScale = 0f;
        
        bool isPlayerClimbing = Mathf.Abs(rb2D.velocity.y) > Mathf.Epsilon;
        anim.SetBool("Climbing", isPlayerClimbing);
    }
    private void Die()
    {
        if (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))
            || feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            anim.SetTrigger("Dying");
            rb2D.velocity = deathKick;
            StartCoroutine(FindObjectOfType<GameSession>().ProcessPlayerDeath());
        }
    }

    private void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;
        if (isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2D.velocity.x), 1f);
        }
    }
}

