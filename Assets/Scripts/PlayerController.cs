using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int health = 5;

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float maxSpeed = 30f;
    [SerializeField] float secondJumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f,25f);

    //State
    bool isAlive = true;
    bool isHurted = false;
    bool canJump = true;
    bool canDoubleJump = true;

    //Cached component referenced
    [SerializeField]  PhysicsMaterial2D mat;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D bodyCollider2D;
    CircleCollider2D feetCollider2D;

    float gravityScaleAtStart;

    void Start()
    {
        Time.timeScale = 1f;
        
        //Get References to vriables
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<CircleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        //Stops all the player abilities
        if (!isAlive) { return; }
        if (isHurted) { return; }


        Run();
        Jump();
        ClimbLadder();
        Die();
        FlipSprite();

    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Running", isPlayerMoving);

    }
    private void Jump()
    {
        if (!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            if(canDoubleJump && Input.GetButtonDown("Jump"))
                {
                    Vector2 jumpVelocityToAdd = new Vector2(0f, secondJumpSpeed);
                    rb.velocity = new Vector2(0,0);
                    rb.velocity += jumpVelocityToAdd;
                    canDoubleJump = false;
                }

            canJump = true;
            return; 
        }

        canDoubleJump = true;
        if (Input.GetButton("Jump") && canJump)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity += jumpVelocityToAdd;
            canJump = false;
        }
    }
    private void ClimbLadder()
    {
        if (!bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            anim.SetBool("Climbing", false);
            rb.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rb.velocity.x, controlThrow * climbSpeed);
        rb.velocity = climbVelocity;

        rb.gravityScale = 0f;
        
        bool isPlayerClimbing = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        anim.SetBool("Climbing", isPlayerClimbing);
    }
    private void Recovery()
    {
        isHurted = false;
        anim.SetTrigger("Hurt");

    }
    private void Die()
    {
        if (health <= 1 && (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))
       || feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))))
        {
            isAlive = false;
            anim.SetTrigger("Dying");

            rb.velocity += new Vector2(deathKick.x * -transform.localScale.x, deathKick.y);
            bodyCollider2D.sharedMaterial = mat;
            FindObjectOfType<GameSession>().TakeLife();
            StartCoroutine(FindObjectOfType<GameSession>().ProcessPlayerDeath());
        }
        else if (!isHurted && (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))
       || feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))))
        {
            health--;
            isHurted = true;
            anim.SetTrigger("Hurt");

            rb.velocity += new Vector2(deathKick.x * -transform.localScale.x, deathKick.y);
            FindObjectOfType<GameSession>().TakeLife();
            Invoke("Recovery", 0.5f);
        }

    }

    private void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}

