using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Transform gizmoOrigin;

    private void OnDrawGizmos()     // aoe radius debug
    {
        Gizmos.color = Color.yellow;
        if (gameObject.name == "troll_placeholder") Gizmos.DrawWireSphere(gizmoOrigin.position, radius);
    }
}
