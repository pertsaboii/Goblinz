using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEbuilding : MonoBehaviour
{
    private enum State
    {
        WalkToMiddle, ChaseTarget, Attack, Roaming
    }

    private State state;
    private GameObject target;

    void Start()
    {
        if (gamemanager.buildings.Count != 0)
        {
            foreach (GameObject building in gamemanager.buildings)
            {
                if (target == null || Vector3.Distance(building.transform.position, gameObject.transform.position) < Vector3.Distance(target.transform.position, gameObject.transform.position))
                {
                    target = building;
                }
            }
        }
    }

    void Update()
    {

    }
}
