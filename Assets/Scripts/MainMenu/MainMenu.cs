using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("FINAL LEVEL 1");
    }
    public void Settings()
    {
        SceneManager.LoadScene("MenuOptions");
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void SetQuality (int qualitySettings) 
    {
        QualitySettings.SetQualityLevel(qualitySettings);
    }
    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen= isFullscreen;
    }

}
