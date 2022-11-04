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

    public static List<GameObject> buildings;
    public List<GameObject> talot;

    private GameObject loseCon;

    private void Awake()
    {
        camera = Camera.main;

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
                break;
            case State.Pause:
                break;
            case State.GameOver:
                break;
            case State.HalfTime:
                break;
            case State.DoubleTime:
                break;
        }
    }
}
