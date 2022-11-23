using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_Spear : MonoBehaviour
{
    [HideInInspector] public bool canDamage;
    [HideInInspector] public float sweepDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (canDamage == true && (other.gameObject.layer == 7 || other.gameObject.layer == 8)) other.gameObject.GetComponent<ALL_Health>().UpdateHealth(-sweepDamage);
    }
}
