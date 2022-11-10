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
    private Animator anim;

    [SerializeField] private GameObject healingCircle;
    void Start()
    {
        gamemanager.buildings.Add(gameObject);
        gamemanager.buildingsAndUnits.Add(gameObject);
        healingBarSprite.fillAmount = 0;
        anim = GetComponent<Animator>();

        healingCircle.transform.localScale = new Vector3(healingRange * 2, 0.2f, healingRange * 2);
    }

    void Update()
    {
        healingBarSprite.fillAmount += Time.deltaTime / healingInterval;

        if (healingBarSprite.fillAmount >= 1)
        {
            healingBarSprite.fillAmount = 0;
            Heal();
        }

        healingBar.transform.LookAt(new Vector3(1, gamemanager.camera.transform.position.y * 10, gamemanager.camera.transform.position.z * 10));
    }
    void Heal()
    {
        anim.SetTrigger("Heal");

        Collider[] colliders = Physics.OverlapSphere(transform.position, healingRange, layerMask);
        if (colliders != null)
        {
            foreach (Collider col in colliders)
            {
                if (col.gameObject.CompareTag("Unit")) col.gameObject.GetComponent<ALL_Health>().UpdateHealth(1);
            }
        }
    }
}
