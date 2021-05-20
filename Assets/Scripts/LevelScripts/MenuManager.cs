using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Success")
        {
            if(pauseMenu.activeSelf)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
