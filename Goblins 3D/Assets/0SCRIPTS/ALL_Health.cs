using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALL_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;

    [SerializeField] private Image hpSprite;
    [SerializeField] private GameObject hpBar;

    public GameObject unitThatSpawns;
    [SerializeField] private bool spawnsUnit;
    [SerializeField] private bool isBuilding;
    [SerializeField] private bool dealsDmgOnDeath;

    [HideInInspector] public float deathDmgRadius;
    [SerializeField] private LayerMask deathDmgTargetType;
    [HideInInspector] public float deathDamage;
    void Start()
    {
        currentHealth = maxHealth;
        hpSprite.fillAmount = currentHealth / maxHealth;
        hpBar.SetActive(false);
    }
    public void UpdateHealth(float healthChange)
    {
        if (currentHealth <= maxHealth) hpBar.SetActive(true);
        currentHealth += healthChange;
        hpSprite.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0) Death();
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            hpBar.SetActive(false);
        }
    }
    private void Update()
    {
        hpBar.transform.LookAt(new Vector3(1, gamemanager.camera.transform.position.y * 10, gamemanager.camera.transform.position.z * 10));
    }
    void Death()
    {
        if (gameObject.CompareTag("Building") == true) gamemanager.buildings.Remove(gameObject);
        if (spawnsUnit == true) Instantiate(unitThatSpawns, transform.position, Quaternion.identity);
        if (isBuilding == true) gamemanager.buildings.Remove(this.gameObject);
        if (dealsDmgOnDeath)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, deathDmgRadius, deathDmgTargetType);
            if (colliders != null) foreach (Collider col in colliders) col.gameObject.GetComponent<ALL_Health>().UpdateHealth(-deathDamage);
        }
        Destroy(gameObject);
    }
}
