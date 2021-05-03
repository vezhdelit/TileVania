using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingRight())
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }
    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
    void OnTriggerExit2D(Collider2D collision)  
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
    }   

}
