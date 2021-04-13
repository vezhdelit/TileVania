using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingRight())
        {
            rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
        }
    }
    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
    void OnTriggerExit2D(Collider2D collision)  
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb2D.velocity.x), 1f);
    }   

}
