using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    void Start()
    {
        gamemanager.buildingsAndUnits.Add(gameObject);
    }
}
