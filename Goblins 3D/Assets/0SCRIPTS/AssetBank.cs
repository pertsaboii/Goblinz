using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBank : MonoBehaviour
{
    public enum FXType
    {
        BatteringRamHit,
        GobboUnitDeath,
        BuildingDeath,
        BasicEnemyDeath,
        WanderingDeath,
        TrollUnitDeath,
    }
    public FX[] FXArray;

    [System.Serializable]
    public class FX
    {
        public ParticleSystem particleSystem;
        public FXType fXType;
    }
    public ParticleSystem FindFX(FXType type)
    {
        foreach (FX fx in FXArray)
        {
            if (fx.fXType == type)
            {
                return fx.particleSystem;
            }
        }
        return null;
    }




}
