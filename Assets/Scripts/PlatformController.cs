using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] bool useCircleMovement = false;
    [SerializeField] float radius = 2f;
    [SerializeField] float angularSpeed = 2f;
    float angle = 0f;
    private Vector2 center;
    void Start()
    {
        center = transform.position;
    }

    void Update()
    {
        if(useCircleMovement)
            CircleMovement();
    }
    void CircleMovement()
    {
        float posX = center.x + Mathf.Cos(angle) * radius;
        float posY = center.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector2(posX, posY);
        angle += Time.deltaTime * angularSpeed;

        if (angle >= 360f) { angle = 0f; }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("1");
            other.gameObject.transform.parent = this.transform.transform;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("1");
            other.gameObject.transform.parent = this.transform.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("2");
            other.gameObject.transform.parent = null;
        }
    }
}
