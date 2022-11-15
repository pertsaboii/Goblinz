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
        if (gameObject.name == "U_tankgobbo") Gizmos.DrawWireSphere(gizmoOrigin.position, radius);
        else if (gameObject.name == "ph_wanderinggobbo") Gizmos.DrawWireSphere(transform.position, radius);
        else if (gameObject.name == "ph_healbuilding") Gizmos.DrawWireSphere(new Vector3(transform.position.x, 0, transform.position.z), radius);
    }
}
