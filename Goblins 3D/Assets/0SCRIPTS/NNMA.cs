using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNMA : MonoBehaviour
{
    private enum State
    {
        NotAttacking, Attacking
    }
    private State state;

    public bool targetInRange;
    private Animator anim;

    [SerializeField] private float attackSpeed;

    [SerializeField] private int attackDamage;
    [HideInInspector] public GameObject target;
    [HideInInspector] public health targetHealth;

    [SerializeField] private string currentState;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    void Start()
    {
        anim = GetComponent<Animator>();
        state = State.NotAttacking;
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.NotAttacking:
                if (target != null && targetInRange == true) SwitchToAttackState();
                break;
            case State.Attacking:
                anim.SetInteger("State", 2);
                if (target == null || targetInRange == false) state = State.NotAttacking;
                if (target == null) targetInRange = false;
                break;
        }
        currentState = state.ToString();
    }
    public void SwitchToAttackState()
    {
        targetInRange = true;
        anim.SetFloat("AttackSpeed", attackSpeed);
        state = State.Attacking;
    }
    void SingleTargetMeleeDmg()
    {
        if (target != null && targetInRange == true) targetHealth.UpdateHealth(-attackDamage);
        else targetInRange = false;
    }
    void SpawnProjectile()
    {
        GameObject spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectile projectile = spawnedProjectile.GetComponent<projectile>();
        projectile.target = target;
        projectile.targetHealth = targetHealth;
    }
}
