using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_HealingBuilding : MonoBehaviour
{
    [SerializeField] private int healingInterval;
    [SerializeField] private float healAmount;
    [SerializeField] private float healingRange;
    [SerializeField] private Image healingBarSprite;
    [SerializeField] private GameObject healingBar;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private ParticleSystem ps;
    private ALL_Health healthScript;

    void Start()
    {
        gamemanager.buildings.Add(gameObject);
        gamemanager.buildingsAndUnits.Add(gameObject);
        healingBarSprite.fillAmount = 0;
        healthScript = GetComponent<ALL_Health>();
        ParticleSystem.ShapeModule psShape = ps.shape;
        psShape.radius = healingRange;
    }

    void Update()
    {
        healingBarSprite.fillAmount += Time.deltaTime / healingInterval;

        if (healingBarSprite.fillAmount >= 1)
        {
            healingBarSprite.fillAmount = 0;
            Heal();
        }

        healingBar.transform.LookAt(new Vector3(transform.position.x - gamemanager.camera.transform.position.x, gamemanager.camera.transform.position.y * 10, transform.position.z - gamemanager.camera.transform.position.z));

        if (healthScript.isDead == true) healingBar.SetActive(false);   // jos suorituskykyongelmia niin tämä johkin voidiin
    }
    void Heal()
    {
        ps.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, healingRange, layerMask);
        if (colliders != null)
        {
            foreach (Collider col in colliders)
            {
                if (col.gameObject != gameObject) col.gameObject.GetComponent<ALL_Health>().UpdateHealth(healAmount);
            }
        }
    }
}
