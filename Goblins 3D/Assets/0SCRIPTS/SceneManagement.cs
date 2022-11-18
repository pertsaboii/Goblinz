using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        gamemanager.userInterface.ButtonClickAudio();
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        gamemanager.userInterface.ButtonClickAudio();
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        gamemanager.userInterface.ButtonClickAudio();
        Application.Quit();
    }
}
