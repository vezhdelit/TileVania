using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        //Singleton pattern / Has many realizations
        //Makes only one original instance through all the levels
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
