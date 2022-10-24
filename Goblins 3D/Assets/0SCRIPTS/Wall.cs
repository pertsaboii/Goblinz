using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void Start()
    {
        gamemanager.buildings.Add(this.gameObject);
    }
}
