using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    [SerializeField] float platformFallDelay = 2f;
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(PlatformFall());
    }

    IEnumerator PlatformFall()
    {
        GetComponent<Animator>().SetTrigger("boom");

        yield return new WaitForSecondsRealtime(platformFallDelay);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        
        yield return new WaitForSecondsRealtime(platformFallDelay * 3);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
