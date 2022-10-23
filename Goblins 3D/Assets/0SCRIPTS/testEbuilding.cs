using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ObstacleAgent))]
public class testEbuilding : MonoBehaviour
{
    private enum State
    {
        ApproachTarget, Attack, Roaming
    }

    private State state;
    private GameObject target;

    private ObstacleAgent agent;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;

    [SerializeField] private GameObject spawningEnemy;

    private int currentBuildingAmount;

    private health healthScript;

    void Start()
    {
        healthScript = GetComponent<health>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        agent = GetComponent<ObstacleAgent>();
        LockOnTarget();
    }

    void LockOnTarget()
    {
        if (gamemanager.buildings.Count != 0)
        {
            currentBuildingAmount = gamemanager.buildings.Count;

            foreach (GameObject building in gamemanager.buildings)
            {
                if (target == null || Vector3.Distance(building.transform.position, gameObject.transform.position) < Vector3.Distance(target.transform.position, gameObject.transform.position))
                {
                    target = building;
                    state = State.ApproachTarget;
                    agent.SetDestination(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                }
            }
        }
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.ApproachTarget:
                if (currentBuildingAmount != gamemanager.buildings.Count) LockOnTarget();
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.ApproachTarget && collision.gameObject == target)
        {           
            ImpactToTarget();
        }
    }
    void ImpactToTarget()
    {
        target.GetComponent<health>().UpdateHealth(-attackDamage);
        healthScript.UpdateHealth(-healthScript.currentHealth -1);
    }
}
