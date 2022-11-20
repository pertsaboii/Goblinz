using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        gamemanager.userInterface.ButtonClickAudio();
        if (MultiScene.multiScene.purchasedCards.Count != 0)
        {
            SoundManager.Instance.musicSounds.Stop();
            SceneManager.LoadScene(1);
        }
        else StartCoroutine(gamemanager.userInterface.CannotStartGame());
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
