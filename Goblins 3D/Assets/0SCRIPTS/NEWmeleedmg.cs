using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWmeleedmg : MonoBehaviour
{
    [HideInInspector] public bool attackStateOn;
    [HideInInspector] public bool targetDead;
    private Animator anim;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int attackDamage;
    [HideInInspector] public GameObject target;
    health targetHealth;
    [HideInInspector] public bool targetInRange;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public IEnumerator Attack()
    {
        if (target != null)
        {
            targetDead = false;
            attackStateOn = true;
            targetHealth = target.GetComponent<health>();
            anim.SetFloat("AttackSpeed", attackSpeed);

            while (target != null && targetInRange == true)
            {
                anim.SetInteger("State", 3);
                anim.SetInteger("State", 2);

                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 0.5f);

                if (target != null && targetInRange == true) targetHealth.UpdateHealth(-attackDamage);

                if (target == null)
                {
                    targetDead = true;
                    attackStateOn = false;
                    targetInRange = false;
                    yield break;
                }

                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 0.5f);    
            }
            if (target == null)
            {
                targetDead = true;
                attackStateOn = false;
                targetInRange = false;
            }
        }
    }
}
