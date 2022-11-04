using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uimanager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject runTimeUi;

    private void Start()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void PauseMenuOnOff()
    {
        if (pauseMenu.activeSelf == false) pauseMenu.SetActive(true);
        else pauseMenu.SetActive(false);

    }
    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
    public void DisableRunTimeUI()
    {
        runTimeUi.SetActive(false);
    }
}
