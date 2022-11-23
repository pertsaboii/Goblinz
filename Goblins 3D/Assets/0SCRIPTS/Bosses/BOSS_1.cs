using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BOSS_1 : MonoBehaviour
{
    private enum State
    {
        Spawn, WalkToChief, MeleeAttacking
    }
    private State state;

    [Header("Setup")]
    [SerializeField] private GameObject spear;
    [SerializeField] private LayerMask dropLayerMask;
    private B1_Spear spearScript;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private Rigidbody rb;
    private GameObject chief;
    private CameraShake cameraShake;
    private ALL_Health targetHealth;
    private EnemyUnit baseScript;

    [Header("Stats")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkAnimMult;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runAnimMult;
    [SerializeField] private float sweepDamage;
    [SerializeField] private float sweepAttackRange;
    [SerializeField] private float throwDamage;
    [SerializeField] private float throwAttackRange;
    [SerializeField] private float dropDamage;
    [SerializeField] private float dropDamageRadius;

    [Header("Debug")]
    public GameObject target = null;
    [SerializeField] private string currentState;

    [Header("Look At Turn Speed")]
    [SerializeField] private float turnSpeed;
    private Vector3 targetDir;
    private Transform localTransform;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        baseScript = GetComponent<EnemyUnit>();
        chief = gamemanager.loseCon;
        cameraShake = gamemanager.mainCineCam.GetComponent<CameraShake>();
        anim.SetFloat("WalkSpeed", walkSpeed * walkAnimMult);
        anim.SetFloat("RunSpeed", runSpeed * runAnimMult);
        localTransform = GetComponent<Transform>();
        spearScript = spear.GetComponent<B1_Spear>();
        spearScript.sweepDamage = sweepDamage;
        state = State.Spawn;

        Invoke("DropFromSky", 6f);
    }
    
    void Update()
    {
        switch (state)
        {
            default:
            case State.Spawn:
                break;
            case State.WalkToChief:
                LookAtTarget();
                ScanArea();
                break;
            case State.MeleeAttacking:
                LookAtTarget();
                break;
        }
        currentState = state.ToString();
        if (state != State.Spawn && rb.velocity != Vector3.zero) rb.velocity = Vector3.zero;
        if (target != null)
            if (targetHealth.isDead == true) target = null;
    }
    void DropFromSky()
    {
        rb.useGravity = true;
        rb.AddForce(Vector3.down * 7000, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Spawn && collision.gameObject.layer == 10)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, dropDamageRadius, dropLayerMask);
            {
                if (cols.Length != 0) foreach (Collider col in cols)
                    {
                        col.gameObject.GetComponent<ALL_Health>().UpdateHealth(-dropDamage);
                    }
            }
            anim.SetTrigger("Land");
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            gameObject.layer = 6;
            Instantiate(gamemanager.assetBank.FindFX(AssetBank.FXType.GroundDustWave), transform.position, Quaternion.identity);
            cameraShake.StartShakeCamera();
        }
    }
    void SpawnDone()    // callataan land-animaation lopussa
    {
        navMeshAgent.enabled = true;
        if (navMeshAgent.isStopped == true) navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        state = State.WalkToChief;
        navMeshAgent.SetDestination(chief.transform.position);
    }
    void StartWalkToChief()
    {
        if (navMeshAgent.enabled == false) navMeshAgent.enabled = true;
        anim.SetTrigger("Walk");
        state = State.WalkToChief;
        navMeshAgent.speed = walkSpeed;
        navMeshAgent.SetDestination(chief.transform.position);
    }
    void ScanArea()
    {
        foreach (GameObject unitOrBuilding in gamemanager.buildingsAndUnits)
        {
            if (Vector3.Distance(unitOrBuilding.transform.position, transform.position) < sweepAttackRange && (target == null || targetHealth.isDead == true || Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(unitOrBuilding.transform.position, transform.position)))
            {
                target = unitOrBuilding;
                baseScript.target = target;
                targetHealth = target.GetComponent<ALL_Health>();
            }
            if (target != null)     // jos lyö herkästi yhen ylimääräisen niin voi lisätä isDead checkin
            {
                if (state != State.MeleeAttacking) StartMeleeAttacking();
                return;
            }
        }
        if (state != State.WalkToChief)
        {
            target = null;
            StartWalkToChief();
        }
    }
    void StartMeleeAttacking()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        anim.SetTrigger("Sweep");
        state = State.MeleeAttacking;
    }
    void LookAtTarget()
    {
        if (state == State.WalkToChief) targetDir = chief.transform.position - localTransform.position;
        else if (state == State.MeleeAttacking && target != null) targetDir = target.transform.position - localTransform.position;

        var lookAtTargetRotation = Quaternion.LookRotation(targetDir);

        rb.MoveRotation(Quaternion.RotateTowards(localTransform.rotation, lookAtTargetRotation, turnSpeed));
    }
    void SpearDamageable()
    {
        spearScript.canDamage = !spearScript.canDamage;
    }
}
