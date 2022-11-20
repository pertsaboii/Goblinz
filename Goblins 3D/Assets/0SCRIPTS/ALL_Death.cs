using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ALL_Death : MonoBehaviour
{
    private enum DeathFXType
    {
        GobboUnitDeath, EnemyDeath, WanderingDeath, TrollDeath, BuildingDeath
    }
    [Header("General")]
    private float deathAnimTime = 0.15f;
    [SerializeField] private DeathFXType deathFXType;
    [Header("Unit Spawn Upon Death")]
    [SerializeField] private bool spawnsUnitOnDeath;
    [SerializeField] private GameObject spawningUnit;
    [SerializeField] private Transform spawnPoint;
    [Header("AoE Dmg Upon Death")]
    [SerializeField] private bool dealsAoeDmgOnDeath;
    [SerializeField] private float deathDmgAmount;
    [SerializeField] private float deathDmgRadius;
    [SerializeField] private LayerMask deathDmgLayerMask;
    void Start()
    {
        if (spawnPoint == null) spawnPoint = transform;
    }

    public IEnumerator Death()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            MultiScene.multiScene.money += gameObject.GetComponent<EnemyUnit>().value * MultiScene.multiScene.moneyMult;
            gamemanager.userInterface.UpdateMoneyText();
        }
        RemoveLayerAndTag(gameObject);
        //gameObject.transform.DOScale(transform.localScale * 1.2f, deathAnimTime).SetEase(Ease.OutBounce);     // eri tyylejä death bouncelle
        //gameObject.transform.DOScale(0, deathAnimTime).SetEase(Ease.InBounce);
        gameObject.transform.DOPunchScale(transform.localScale * .3f, deathAnimTime, 5, 0.1f);
        yield return new WaitForSeconds(deathAnimTime / 2);

        InstantiateDeathFX();

        yield return new WaitForSeconds(deathAnimTime / 2);

        if (spawnsUnitOnDeath == true) Instantiate(spawningUnit, transform.position, Quaternion.identity);
        if (dealsAoeDmgOnDeath == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, deathDmgRadius, deathDmgLayerMask);
            if (colliders != null) foreach (Collider col in colliders) col.gameObject.GetComponent<ALL_Health>().UpdateHealth(-deathDmgAmount);
        }
        Destroy(gameObject);
    }
    void InstantiateDeathFX()
    {
        if (deathFXType == DeathFXType.GobboUnitDeath) Instantiate(gamemanager.assetBank.FindFX(AssetBank.FXType.GobboUnitDeath), transform.position, Quaternion.identity);
        else if (deathFXType == DeathFXType.EnemyDeath) Instantiate(gamemanager.assetBank.FindFX(AssetBank.FXType.BasicEnemyDeath), transform.position, Quaternion.identity);
        else if (deathFXType == DeathFXType.TrollDeath) Instantiate(gamemanager.assetBank.FindFX(AssetBank.FXType.TrollUnitDeath), transform.position, Quaternion.identity);
        else if (deathFXType == DeathFXType.WanderingDeath) Instantiate(gamemanager.assetBank.FindFX(AssetBank.FXType.WanderingDeath), transform.position, Quaternion.identity);
    }
    void RemoveLayerAndTag(GameObject go)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 0;
            trans.gameObject.tag = "Untagged";
        }
    }
}
