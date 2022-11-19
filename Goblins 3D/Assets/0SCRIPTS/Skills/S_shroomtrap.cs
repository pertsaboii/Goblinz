using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S_shroomtrap : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    [SerializeField] private float damage;
    [SerializeField] private float initialDamage;
    [SerializeField] private float damageInterval;
    [SerializeField] private int damageCycles;
    [SerializeField] private float cloudRadius;
    private Collider col;
    private MeshRenderer mesh;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        col = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            col.enabled = false;
            StartCoroutine("DamageCloud");
            other.gameObject.GetComponent<ALL_Health>().UpdateHealth(-initialDamage);
        }
    }
    IEnumerator DamageCloud()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        transform.DOPunchScale(Vector3.one * 0.4f, .15f, 5, 0.1f);

        Invoke("DisableMesh", .15f);

        for (int i = 0; i < damageCycles; i++)
        {
            yield return new WaitForSeconds(damageInterval);

            Collider[] enemies = Physics.OverlapSphere(transform.position, cloudRadius, layerMask);
            if (enemies.Length != 0)
            {
                foreach (Collider enemy in enemies)
                {
                    enemy.gameObject.GetComponent<ALL_Health>().UpdateHealth(-damage);
                }
            }
        }
        Destroy(gameObject);
    }
    void DisableMesh()
    {
        mesh.enabled = false;
        transform.DOScale(Vector3.zero, .1f); // tämän voi ottaa pois sit kun on valmis model
    }
}
