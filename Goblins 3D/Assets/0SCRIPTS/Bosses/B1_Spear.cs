using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class B1_Spear : MonoBehaviour
{
    // melee specs
    [HideInInspector] public bool canDamage;
    [HideInInspector] public float sweepDamage;
    public Collider meleeTriggerCol;

    [Header("Throw")]
    public NavMeshObstacle spearObstacle;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public float throwDamage;
    public Collider onGroundCol;
    public Collider throwTriggerCol;
    [HideInInspector] public bool spearLookAtTarget;
    [HideInInspector] public Vector3 targetPos;
    private Transform localTransform;

    //debug
    [SerializeField] private bool debugMode;
    [SerializeField] private GameObject debugTarget;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        localTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        if (spearLookAtTarget == true || debugMode == true) SpearLookAtTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDamage == true && (other.gameObject.layer == 7 || other.gameObject.layer == 8)) other.gameObject.GetComponent<ALL_Health>().UpdateHealth(-sweepDamage);
        else if (rb.isKinematic == false && (other.gameObject.layer == 7 || other.gameObject.layer == 8))
        {
            throwTriggerCol.enabled = false;
            spearLookAtTarget = false;
            rb.isKinematic = true;
            other.gameObject.GetComponent<ALL_Health>().UpdateHealth(-throwDamage);
            onGroundCol.enabled = true;
            spearObstacle.enabled = true;
        }
    }
    void SpearLookAtTarget()
    {
        if (debugMode == true) targetPos = debugTarget.transform.GetComponent<Collider>().bounds.center;
        var projectileTargetRotation = Quaternion.LookRotation(targetPos - localTransform.position);

        rb.MoveRotation(Quaternion.RotateTowards(localTransform.rotation, projectileTargetRotation, 50));
    }
}
