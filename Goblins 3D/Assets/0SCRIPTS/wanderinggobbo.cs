using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ObstacleAgent))]
public class wanderinggobbo : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private ObstacleAgent agent;
    private NavMeshAgent navMeshAgent;

    private State state;
    [SerializeField] private float moveSpeed;

    private float timeBtwWalks;
    [SerializeField] private float walkCycleTime;
    [SerializeField] private float idleTime;
    private Vector3 originalPos;
    [SerializeField] private float wanderingRange;
    private Vector3 randomPos;

    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionDamage;

    private Animator anim;

    private Rigidbody rb;

    private ALL_Health healthScript;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetFloat("WalkSpeed", moveSpeed / 2);        //--- jos run anim on liian hidas/nopea niin t‰t‰ voi s‰‰t‰‰
        timeBtwWalks = 0;
        state = State.Roaming;
        anim.SetInteger("State", 0);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        agent = GetComponent<ObstacleAgent>();
        originalPos = transform.position;
        healthScript = GetComponent<ALL_Health>();
        healthScript.deathDmgRadius = explosionRange;
        healthScript.deathDamage = explosionDamage;
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                if (Vector3.Distance(randomPos, transform.position) < 0.1f && navMeshAgent.enabled == true) navMeshAgent.ResetPath();
                if (Vector3.Distance(transform.position, originalPos) >= wanderingRange)
                {
                    anim.SetInteger("State", 1);
                    agent.SetDestination(originalPos);
                }
                else if (timeBtwWalks <= 0)
                {
                    StartCoroutine("RandomMovement");
                    timeBtwWalks = walkCycleTime + idleTime;
                }
                else timeBtwWalks -= Time.deltaTime;
                break;
        }

        if (rb.velocity != Vector3.zero) rb.velocity = Vector3.zero;
    }
    IEnumerator RandomMovement()
    {
        randomPos = transform.position;
        if (navMeshAgent.enabled == true) navMeshAgent.ResetPath();
        anim.SetInteger("State", 0);

        yield return new WaitForSeconds(idleTime);

        anim.SetInteger("State", 1);

        while (Vector3.Distance(randomPos, transform.position) < 4) CalculateRandomNavMeshPoint();
        agent.SetDestination(randomPos);
    }
    private void CalculateRandomNavMeshPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderingRange;
        randomDirection += originalPos;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderingRange, 1))
        {
            finalPosition = hit.position;
        }
        randomPos = finalPosition;
    }
}
