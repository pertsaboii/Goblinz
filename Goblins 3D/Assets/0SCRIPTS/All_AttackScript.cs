using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_AttackScript : MonoBehaviour
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
    [HideInInspector] public ALL_Health targetHealth;

    [SerializeField] private string currentState;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    [SerializeField] private Transform aoeDmgOrigin;
    [SerializeField] private float aoeRadius;
    [SerializeField] private LayerMask aoeDmgTargets;
    [SerializeField] private ParticleSystem ps;
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
        if (target != null && targetInRange == true) targetHealth.UpdateHealth(-attackDamage);      // jos haluaa ett? dmg v?littyy targettiin joka on rangen ulkopuolella t?ss? vaiheessa niin targetinrangen voi ottaa pois
        else targetInRange = false;
    }
    void SpawnProjectile()
    {
        GameObject spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectile projectile = spawnedProjectile.GetComponent<projectile>();
        projectile.target = target;
        projectile.targetHealth = targetHealth;
        projectile.whoSpawnedProjectile = this.gameObject;
    }
    void AoeMeleeDmg()
    {
        ps.Play();                                // t?h?n my?hemmin pool
        Collider[] colliders = Physics.OverlapSphere(aoeDmgOrigin.position, aoeRadius, aoeDmgTargets);
        {
            foreach (Collider col in colliders)
            {
                col.GetComponent<ALL_Health>().UpdateHealth(-attackDamage);
            }
        }
    }
}
