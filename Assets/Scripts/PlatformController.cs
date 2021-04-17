using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] [Range(1, 5)] int mode = 2;
    [SerializeField] float distance = 2f;
    [SerializeField] float speed = 2f;

    private float angle = 0f;
    private Vector2 center;
    void Start()
    {
        center = transform.position;
    }

    void FixedUpdate()
    {
        switch (mode)
        {
            case 1:
                HorizontalMovement();
                break;
            case 2:
                VerticalMovement();
                break;
            case 3:
                RightDiagonalMovement();
                break;
            case 4:
                LeftDiagonalMovement();
                break;
            case 5:
                CircleMovement();
                break;
            default:
                Debug.LogError("No mode selected");
                break;
        }
    }

    void HorizontalMovement()
    {
        if (transform.position.x >= center.x + distance || transform.position.x <= center.x - distance)
        {
            speed *= -1;
        }
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }
    void VerticalMovement()
    {
        if (transform.position.y >= center.y + distance || transform.position.y <= center.y - distance)
        {
            speed *= -1;
        }

        transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
    }

    void RightDiagonalMovement()
    {
        if (transform.position.x >= center.x + distance || transform.position.x <= center.x - distance)
        {
            speed *= -1;
        }
        transform.position += new Vector3(speed, speed, 0) * Time.deltaTime;
    }
    void LeftDiagonalMovement()
    {
        if (transform.position.x >= center.x + distance || transform.position.x <= center.x - distance)
        {
            speed *= -1;
        }
        transform.position += new Vector3(-speed, speed, 0) * Time.deltaTime;
    }
    void CircleMovement()
    {
        float posX = center.x + Mathf.Cos(angle) * distance;
        float posY = center.y + Mathf.Sin(angle) * distance;
        transform.position = new Vector2(posX, posY);
        angle += Time.deltaTime * speed;

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
