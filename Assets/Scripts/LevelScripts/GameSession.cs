using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    
    void Awake()
    {
        //Singleton pattern / Has many realizations
        //Makes only one original instance
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Success")
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator ProcessPlayerDeath()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();
    }
    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
