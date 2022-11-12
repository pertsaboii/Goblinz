using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_speartrap : MonoBehaviour
{
    private Animator anim;
    private BoxCollider bc;
    private ALL_Health targetHealth;

    [SerializeField] private float damage;
    void Start()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetHealth = other.gameObject.GetComponent<ALL_Health>();
            bc.enabled = false;
            anim.SetTrigger("Activate");
        }
    }
    public void TrapDamage()
    {
        if (targetHealth.isDead == false) targetHealth.UpdateHealth(-damage);
    }
    public void DestroyTrap()
    {
        Destroy(gameObject);
    }
}
