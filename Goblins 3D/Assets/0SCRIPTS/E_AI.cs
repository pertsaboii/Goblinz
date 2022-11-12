using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ObstacleAgent), typeof(ALL_Health), typeof(All_AttackScript))]
public class E_AI : MonoBehaviour
{
    private enum State
    {
        Null, ChaseTarget, Attack, WalkToMiddle, OutSideOfScreen
    }
    [SerializeField] private State state;

    [SerializeField] private bool isRanged;

    private ObstacleAgent agent;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float targetScanningRange;

    public GameObject target = null;

    [SerializeField] private float attackRange;

    private Animator anim;

    [SerializeField] private LayerMask layerMask;

    private All_AttackScript attackScript;
    private float attackDistance;

    private Rigidbody rb;

    private EnemyUnit baseScript;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetFloat("WalkSpeed", moveSpeed / 2);          //--- jos run anim on liian hidas/nopea niin t‰t‰ voi s‰‰t‰‰
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        agent = GetComponent<ObstacleAgent>();
        attackScript = GetComponent<All_AttackScript>();
        baseScript = GetComponent<EnemyUnit>();
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.OutSideOfScreen:
                break;
            case State.ChaseTarget:
                anim.SetInteger("State", 1); // joskus j‰‰ attack animation p‰‰lle, t‰m‰ est‰‰ sen
                if (target != null) agent.SetDestination(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                if (target == null) StartWalkToMiddle();
                ApproachTarget();
                break;
            case State.Attack:
                if (target != null && Vector3.Distance(target.transform.position, transform.position) > attackDistance + 0.3f) StartChaseState();
                if (navMeshAgent.enabled == true) navMeshAgent.ResetPath();
                if (target == null)
                {
                    if (isRanged == true)
                    {
                        foreach (GameObject unitOrBuilding in gamemanager.buildingsAndUnits)
                        {
                            if (Vector3.Distance(unitOrBuilding.transform.position, transform.position) < attackRange && (target == null || Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(unitOrBuilding.transform.position, transform.position)))
                            {
                                target = unitOrBuilding;
                                attackDistance = Vector3.Distance(target.transform.position, transform.position);
                                attackScript.target = target;
                                attackScript.targetHealth = target.GetComponent<ALL_Health>();
                                attackScript.targetInRange = true;
                            }
                        }
                        if (target == null) StartWalkToMiddle();
                        else attackScript.SwitchToAttackState();
                    }
                    else
                    {
                        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, layerMask);
                        if (colliders != null)
                        {
                            foreach (Collider col in colliders)
                            {
                                if (target == null)
                                {
                                    target = col.gameObject;
                                    attackScript.target = target;
                                    attackScript.targetHealth = target.GetComponent<ALL_Health>();
                                    attackScript.targetInRange = true;
                                    attackScript.SwitchToAttackState();
                                }
                            }
                            if (target == null) StartWalkToMiddle();
                        }
                    }
                }
                if (target != null) transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                break;
            case State.WalkToMiddle:
                anim.SetInteger("State", 1);    // v‰lill‰ anim attack state j‰‰ p‰‰lle, t‰m‰ est‰‰ sen
                ScanArea();
                if (target != null) StartChaseState();
                break;
        }

        if (rb.velocity != Vector3.zero) rb.velocity = Vector3.zero;

        if (attackScript.target != null && attackScript.targetHealth.isDead == true)    // t‰m‰n voisi siirt‰‰ all_healthin death voidiin
        {
            attackScript.target = null;
            target = null;
        }
        if (target != null) baseScript.target = target;                                 // jos on suorituskykyongelmia niin t‰m‰n voi siirt‰‰ voideihin
        else baseScript.target = null;
        
    }
    void StartWalkToScreen()
    {
        anim.SetInteger("State", 1);
        agent.SetDestination(new Vector3(0, transform.position.y, 0));
        state = State.OutSideOfScreen;
    }
    void CheckForEnemies()
    {
        foreach (GameObject unitOrBuilding in gamemanager.buildingsAndUnits)
        {
            if (Vector3.Distance(unitOrBuilding.transform.position, transform.position) < attackRange && (target == null || Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(unitOrBuilding.transform.position, transform.position)))
            {
                target = unitOrBuilding;
                attackScript.target = target;
                attackScript.targetHealth = target.GetComponent<ALL_Health>();
                attackScript.targetInRange = true;
            }
        }
        if (target == null) StartWalkToMiddle();
        else StartAttackState();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyOutsideScreen")) StartWalkToScreen();
        if (other.CompareTag("Enemy Tag Activator")) CheckForEnemies();
    }
    void StartWalkToMiddle()
    {
        anim.SetInteger("State", 1);
        agent.SetDestination(new Vector3(0, transform.position.y, 0));
        state = State.WalkToMiddle;
    }

    void ScanArea()
    {
        foreach (GameObject unitOrBuilding in gamemanager.buildingsAndUnits)
        {
            if (Vector3.Distance(unitOrBuilding.transform.position, transform.position) < targetScanningRange && (target == null || Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(unitOrBuilding.transform.position, transform.position)))
            {
                target = unitOrBuilding;
                attackScript.target = target;
                attackScript.targetHealth = target.GetComponent<ALL_Health>();
                StartChaseState();
            }
        }
    }
    void StartChaseState()
    {
        attackScript.targetInRange = false;
        attackScript.target = target;
        attackScript.targetHealth = target.GetComponent<ALL_Health>();
        anim.SetInteger("State", 1);
        state = State.ChaseTarget;
    }
    void ApproachTarget()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < attackRange) StartAttackState();

        foreach (GameObject unitOrBuilding in gamemanager.buildingsAndUnits)
        {
            if (unitOrBuilding != target && Vector3.Distance(unitOrBuilding.transform.position, transform.position) < attackRange)
            {
                target = unitOrBuilding;
                attackScript.target = target;
                attackScript.targetHealth = target.GetComponent<ALL_Health>();
                StartAttackState();
            }
        }
        /*Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, layerMask);
        if (colliders != null)
        {
            foreach (Collider col in colliders)
            {
                if (col.gameObject == target) StartAttackState();
                else
                {
                    target = col.gameObject;
                    attackScript.target = target;
                    attackScript.targetHealth = target.GetComponent<ALL_Health>();
                    StartAttackState();
                }
            }
        }*/
    }
    void StartAttackState()
    {
        attackDistance = Vector3.Distance(target.transform.position, transform.position);
        state = State.Attack;
        attackScript.SwitchToAttackState();
    }
}