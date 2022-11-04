using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    private enum State
    {
        RunTime, Pause, GameOver, HalfTime, DoubleTime
    }
    private State state;

    public static Camera camera;
    public static uimanager userInterface;

    public static List<GameObject> buildings;
    public List<GameObject> talot;

    private GameObject loseCon;

    private void Awake()
    {
        Time.timeScale = 1;

        camera = Camera.main;
        userInterface = GameObject.Find("UI").GetComponent<uimanager>();

        buildings = new List<GameObject>();
        talot = buildings;
        loseCon = GameObject.Find("LoseCon");

        state = State.RunTime;
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.RunTime:
                if (loseCon == null) GameOver();
                break;
            case State.Pause:
                break;
            case State.GameOver:
                break;
            case State.HalfTime: // jos tehd‰‰n mahdollisuus hidastaa aikaa
                break;
            case State.DoubleTime:  // jos tehd‰‰n mahdollisuus nopeuttaa aikaa
                break;
        }
    }
    void GameOver()
    {
        Time.timeScale = 0;
        state = State.GameOver;
        userInterface.DisableRunTimeUI();
        userInterface.GameOverMenu();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        state = State.Pause;
        userInterface.PauseMenuOnOff();
    }
    public void RunTime()
    {
        Time.timeScale = 1;
        state = State.RunTime;
        userInterface.PauseMenuOnOff();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void ResumeEarlierState()
    {
        // t‰h‰n pausesta palatessa joko runtime, haltime tai doubletime jos tehd‰‰n niille napit
    }
}
