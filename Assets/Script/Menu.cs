using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject settingwindow;

    public string levelToLoal;

   public void startGame()
    {
        SceneManager.LoadScene(levelToLoal);
    }
    public void settingsButton ()
    {
        settingwindow.SetActive(true);
    }
    public void quitGame()
    {
        Application.Quit();
    }

    public void quitSettings()
    {
        settingwindow.SetActive(false);
    }
}
