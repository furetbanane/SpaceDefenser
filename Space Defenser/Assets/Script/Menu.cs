using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public string levelToLoal;

   public void startGame()
    {
        SceneManager.LoadScene(levelToLoal);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
