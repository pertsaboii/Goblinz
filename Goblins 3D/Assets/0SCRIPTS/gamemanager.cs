using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static List<GameObject> buildingsAndUnits;
    public static List<GameObject> enemies;
    public List<GameObject> viholliset;

    public static PlayerCards playercards;

    private GameObject loseCon;

    private void Awake()
    {
        Time.timeScale = 1;

        camera = Camera.main;
        userInterface = GameObject.Find("UI").GetComponent<uimanager>();
        playercards = GetComponent<PlayerCards>();

        buildings = new List<GameObject>();
        enemies = new List<GameObject>();
        viholliset = enemies;
        buildingsAndUnits = new List<GameObject>();
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
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(buildingsAndUnits.Count);
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
    public void ResumeEarlierState()
    {
        // t‰h‰n pausesta palatessa joko runtime, haltime tai doubletime jos tehd‰‰n niille napit
    }
}
