using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AIrigidram : MonoBehaviour
{
    private enum State
    {
        ApproachTarget
    }

    private State state;
    private GameObject target;

    [SerializeField] private float buildingDamage;
    [SerializeField] private float unitColDamage;

    private int currentBuildingAmount;

    private ALL_Health healthScript;

    private Rigidbody rb;

    private float speed;
    [SerializeField] private float startSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationPerS;
    [SerializeField] private float turnSpeed;
    private Vector3 targetDir;
    private Collider targetCollider;
    private Transform localTransform;

    private EnemyUnit baseScript;

    private bool impactDone;

    [SerializeField] private Transform hitFXSpawnPoint;

    private CameraShake cameraShake;

    private bool targetInSight = true;
    private bool triggerColEmpty = true;
    [SerializeField] private LayerMask layerMask;
    private GizmoDrawer gizmoScript;    // vain devausvaiheessa
    [SerializeField] private Transform rayCastPoint;
    private Vector3 rayDir;
    [SerializeField] private float rayRange;
    private float randomX;
    private RaycastHit[] hits;

    void Start()
    {
        speed = startSpeed;
        rb = GetComponent<Rigidbody>();
        healthScript = GetComponent<ALL_Health>();
        localTransform = GetComponent<Transform>();
        baseScript = GetComponent<EnemyUnit>();
        cameraShake = gamemanager.camera.GetComponent<CameraShake>();
        LockOnTarget();
    }

    void LockOnTarget()
    {
        foreach (GameObject building in gamemanager.buildings)
        {
            if (target == null || Vector3.Distance(building.transform.position, gameObject.transform.position) < Vector3.Distance(target.transform.position, gameObject.transform.position))
            {
                target = building;
                state = State.ApproachTarget;
                targetCollider = target.GetComponent<Collider>();
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
                Ray ray = new Ray(rayCastPoint.position, rayDir);
                rayDir = new Vector3(target.transform.position.x - rayCastPoint.position.x, rayCastPoint.position.y, target.transform.position.z - rayCastPoint.position.z);
                RaycastHit hitInfo;
                Debug.DrawRay(ray.origin, ray.direction * rayRange, Color.red, .1f);
                if (Physics.BoxCast(rayCastPoint.position, new Vector3(2.5f, 0f, 0f), rayDir, out hitInfo, Quaternion.identity, rayRange, layerMask, QueryTriggerInteraction.Ignore))
                {
                    Debug.Log(hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject != gameObject && hitInfo.transform.gameObject != target) targetInSight = false;
                    else targetInSight = true;
                }
                else targetInSight = true;
                break;
        }
        if (speed <= maxSpeed) speed += Time.deltaTime * accelerationPerS;

        if (target != null) baseScript.target = target;                                 // jos on suorituskykyongelmia niin tämän voi siirtää voideihin
        else baseScript.target = null;
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            if (targetInSight == true && triggerColEmpty == true)
            {
                Debug.Log("normal movement");

                rb.velocity = localTransform.forward * speed;

                targetDir = target.transform.position - localTransform.position;                   // jos kääntyy vituiksi niin tätä muokkaamalla voi korjata

                var targetRotation = Quaternion.LookRotation(targetDir);

                rb.MoveRotation(Quaternion.RotateTowards(localTransform.rotation, targetRotation, turnSpeed));
            }
            else
            {
                rb.velocity = localTransform.forward * speed;

                targetDir = new Vector3(randomX * 4, transform.position.y, transform.position.z) - localTransform.position;                   // jos kääntyy vituiksi niin tätä muokkaamalla voi korjata

                var targetRotation = Quaternion.LookRotation(targetDir);

                rb.MoveRotation(Quaternion.RotateTowards(localTransform.rotation, targetRotation, turnSpeed));
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.ApproachTarget && collision.gameObject.CompareTag("Building"))
        {
            if (impactDone == false) ImpactToTarget();
        }
        // unitin dmg testi
        else if (collision.collider.CompareTag("Unit"))
        {
            collision.collider.gameObject.GetComponent<ALL_Health>().UpdateHealth(-unitColDamage);
            speed -= 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject != gameObject && other.transform.gameObject != target) triggerColEmpty = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject != gameObject && other.transform.gameObject != target) triggerColEmpty = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject != gameObject && other.transform.gameObject != target) triggerColEmpty = true;
    }
    void ImpactToTarget()
    {
        Instantiate(gamemanager.assetBank.FindFX(AssetBank.FXType.BatteringRamHit), hitFXSpawnPoint.position, Quaternion.identity);
        cameraShake.StartShakeCamera();
        impactDone = true;
        target.GetComponent<ALL_Health>().UpdateHealth(-buildingDamage);
        healthScript.UpdateHealth(-healthScript.currentHealth - 1);
    }
}
