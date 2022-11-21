using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class gamemanager : MonoBehaviour
{
    public enum Scene
    {
        MainMenuScene,
        PlayScene
    }
    [SerializeField] private Scene scene;
    public enum State
    {
        MainMenu, RunTime, Pause, GameOver, HalfTime, DoubleTime
    }
    public static State state;

    public static Camera screenInputCamera;
    public static uimanager userInterface;
    public static GameObject loseCon;
    public static CinemachineVirtualCamera mainCineCam;
    public static SceneManagement sceneManagement;
    [SerializeField] private uimanager UIScript;
    [SerializeField] private Camera screenInputCam;
    [SerializeField] private GameObject oldGobbo;
    [SerializeField] private CinemachineVirtualCamera cineMainCam;

    public static List<GameObject> buildings;
    public static List<GameObject> buildingsAndUnits;
    public static List<GameObject> enemies;

    [Header("For Debugging")]
    public List<GameObject> viholliset;
    public List<GameObject> unititJaBuildingit;
    public List<GameObject> buildingit;

    public static PlayerCards playercards;

    public static AssetBank assetBank;
    private void Awake()
    {
        sceneManagement = GetComponent<SceneManagement>();

        if (scene == Scene.PlayScene)
        {
            Time.timeScale = 1;

            screenInputCamera = screenInputCam;
            mainCineCam = cineMainCam;
            userInterface = UIScript;
            loseCon = oldGobbo;
            playercards = GetComponent<PlayerCards>();
            assetBank = GetComponent<AssetBank>();

            buildings = new List<GameObject>();
            enemies = new List<GameObject>();
            buildingsAndUnits = new List<GameObject>();

            // debuggaukseen
            viholliset = enemies;
            unititJaBuildingit = buildingsAndUnits;
            buildingit = buildings;

            state = State.RunTime;
            AudioListener.pause = false;
        }
        else
        {
            Time.timeScale = 1;
            assetBank = GetComponent<AssetBank>();
            userInterface = UIScript;

            state = State.MainMenu;
        }

    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.MainMenu:
                break;
            case State.RunTime:
                if (loseCon == null) GameOver();
                break;
            case State.Pause:
                break;
            case State.GameOver:
                break;
            case State.HalfTime: // jos tehd��n mahdollisuus hidastaa aikaa
                break;
            case State.DoubleTime:  // jos tehd��n mahdollisuus nopeuttaa aikaa
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

        // t�nne my�hemmin void joka aktivoi cheering animaatiot vihollisilla
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        state = State.Pause;
        userInterface.PauseMenuOnOff();
    }
    public void RunTime()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        state = State.RunTime;
        userInterface.PauseMenuOnOff();
    }
    public void ResumeEarlierState()
    {
        // t�h�n pausesta palatessa joko runtime, haltime tai doubletime jos tehd��n niille napit
    }
}
