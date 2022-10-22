using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static Camera camera;

    public static List<GameObject> buildings;

    private void Awake()
    {
        camera = Camera.main;

        buildings = new List<GameObject>();
    }
}
