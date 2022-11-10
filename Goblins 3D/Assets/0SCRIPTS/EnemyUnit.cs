using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public float unitCost;

    private void Start()
    {
        gamemanager.enemies.Add(gameObject);
    }
}
