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
        if(SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Success" || SceneManager.GetActiveScene().name == "Game Over")
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator ProcessPlayerDeath()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        GameOver();  
    }
    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();
    }
    public void TakeLife()
    {
        if (playerLives > 0)
        {
            playerLives--;
            livesText.text = playerLives.ToString();
        }
    }
    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }
    private void GameOver()
    {
        SceneManager.LoadScene("Game Over");
        Destroy(gameObject);
    }


    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
