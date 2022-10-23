using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWmeleedmg : MonoBehaviour
{
    public bool attackStateInitiated;
    public bool targetDead;
    public bool targetInRange;
    private Animator anim;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int attackDamage;
    [HideInInspector] public GameObject target;
    health targetHealth;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public IEnumerator Attack()
    {
        if (target != null)
        {
            if (gameObject.CompareTag("Enemy")) Debug.Log("attak");
            targetDead = false; 
            targetHealth = target.GetComponent<health>();
            anim.SetFloat("AttackSpeed", attackSpeed);

            while (target != null && targetInRange == true)
            {
                attackStateInitiated = true;
                anim.SetInteger("State", 3);
                anim.SetInteger("State", 2);

                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 0.5f);

                if (target != null && targetInRange == true) targetHealth.UpdateHealth(-attackDamage);

                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 0.5f);

                if (target == null)
                {
                    targetDead = true;
                    attackStateInitiated = false;
                    targetInRange = false;
                    yield break;
                }
                else if (targetInRange == false)
                {
                    targetDead = false;
                    attackStateInitiated = false;
                    yield break;
                }
            }
            if (target == null)
            {
                targetDead = true;
                attackStateInitiated = false;
                targetInRange = false;
            }
        }
    }
}
