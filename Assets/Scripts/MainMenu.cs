using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : HoangBehavior
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void QuitMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    
}
