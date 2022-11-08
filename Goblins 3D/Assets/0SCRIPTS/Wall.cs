using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private bool lookAtMiddle;
    void Start()
    {
        gamemanager.buildings.Add(this.gameObject);
        if (lookAtMiddle == true) gameObject.transform.LookAt(new Vector3(0, transform.position.y, 0));
    }
}
