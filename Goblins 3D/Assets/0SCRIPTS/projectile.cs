using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [HideInInspector] public health targetHealth;
    public GameObject target;
    private Rigidbody rb;
    private Vector3 targetDir;
    private Collider targetCollider;
    private Transform localTransform;

    [SerializeField] private LayerMask targetLayer;

    private Collider ragdollCollider;
    [SerializeField] private Collider triggerCol;
    void Start()
    {
        ragdollCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        targetCollider = target.GetComponent<Collider>();
        localTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            rb.velocity = localTransform.forward * speed;

            targetDir = targetCollider.bounds.center - localTransform.position;                   // jotta ei osuisi jalkoihin

            var projectileTargetRotation = Quaternion.LookRotation(targetDir);

            rb.MoveRotation(Quaternion.RotateTowards(localTransform.rotation, projectileTargetRotation, turnSpeed));
        }
    }
    private void Update()
    {
        if (target == null) StartCoroutine(TargetlessProjectile());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target && ((1 << other.gameObject.layer) & targetLayer) != 0) ProjectileHit();
        else if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            targetHealth = other.gameObject.GetComponent<health>();
            ProjectileHit();
        }
        else return;
    }
    void ProjectileHit()
    {
        targetHealth.UpdateHealth(-damage);
        Destroy(gameObject);
    }
    IEnumerator TargetlessProjectile()
    {
        triggerCol.enabled = false;
        rb.useGravity = true;
        ragdollCollider.enabled = true;

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
