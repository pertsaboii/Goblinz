using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [HideInInspector] public float currentHealth;

    [SerializeField] private Image hpSprite;
    [SerializeField] private GameObject hpBar;

    [SerializeField] private GameObject unitThatSpawns;
    [SerializeField] private bool spawnsUnit;
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
        Destroy(gameObject);
    }
}
