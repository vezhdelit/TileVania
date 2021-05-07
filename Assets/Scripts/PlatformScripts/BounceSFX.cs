using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BounceSFX : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D other)
    {
        GetComponent<AudioSource>().Play();
    }

}
