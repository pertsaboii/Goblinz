using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public enum State
    {
        RunTime, Pause, GameOver, HalfTime, DoubleTime
    }
    public static State state;

    public static Camera camera;
    public static uimanager userInterface;
    public static GameObject loseCon;
    [SerializeField] private uimanager UIScript;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject oldGobbo;

    public static List<GameObject> buildings;
    public static List<GameObject> buildingsAndUnits;
    public static List<GameObject> enemies;
    public List<GameObject> viholliset;

    public static PlayerCards playercards;

    public static AssetBank assetBank;
    private void Awake()
    {
        Time.timeScale = 1;

        camera = mainCam;
        userInterface = UIScript;
        loseCon = oldGobbo;
        playercards = GetComponent<PlayerCards>();
        assetBank = GetComponent<AssetBank>();

        buildings = new List<GameObject>();
        enemies = new List<GameObject>();
        viholliset = enemies;
        buildingsAndUnits = new List<GameObject>();

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

        // t‰nne myˆhemmin void joka aktivoi cheering animaatiot vihollisilla
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
