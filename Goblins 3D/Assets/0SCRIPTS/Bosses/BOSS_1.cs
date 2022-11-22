using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BOSS_1 : MonoBehaviour
{
    private enum State
    {
        Spawn, WalkToChief
    }
    private State state;

    // Setup
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private Rigidbody rb;
    private All_AttackScript attackScript;
    private GameObject chief;
    private CameraShake cameraShake;

    [Header("Stats")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkAnimMult;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runAnimMult;
    [SerializeField] private float sweepAttackRange;
    [SerializeField] private float throwAttackRange;

    [Header("Debug")]
    public GameObject target = null;
    [SerializeField] private string currentState;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackScript = GetComponent<All_AttackScript>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        chief = gamemanager.loseCon;
        cameraShake = gamemanager.mainCineCam.GetComponent<CameraShake>();
        anim.SetFloat("WalkSpeed", walkSpeed * walkAnimMult);
        anim.SetFloat("RunSpeed", runSpeed * runAnimMult);

        rb.AddForce(Vector3.down * 5000, ForceMode.Impulse);
    }

    
    void Update()
    {
        switch (state)
        {
            default:
            case State.Spawn:
                break;
        }
        currentState = state.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Spawn && collision.gameObject.layer == 10)
        {
            anim.SetTrigger("Land");
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            Instantiate(gamemanager.assetBank.FindFX(AssetBank.FXType.GroundDustWave), transform.position, Quaternion.identity);
            cameraShake.StartShakeCamera();
        }
    }
    void SpawnDone()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.speed = walkSpeed;
        state = State.WalkToChief;
        navMeshAgent.SetDestination(chief.transform.position);
    }
    void StartWalkToChief()
    {
        anim.SetTrigger("Walk");
        state = State.WalkToChief;
        navMeshAgent.speed = walkSpeed;
        navMeshAgent.SetDestination(chief.transform.position);
    }
}
