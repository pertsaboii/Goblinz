using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScene : MonoBehaviour
{
    public static MultiScene multiScene;

    [HideInInspector] public int difficulty;

    [HideInInspector] public float highScore;
    [HideInInspector] public float money;
    [SerializeField] private float easyModeMoneyMult;
    [SerializeField] private float hardModeMoneyMult;
    [HideInInspector] public float moneyMult;
    private bool difficultyUpdatedFirstTime;
    private void Awake()
    {
        if (multiScene == null)
        {
            DontDestroyOnLoad(gameObject);
            multiScene = this;
        }
        else if (multiScene != this)
        {
            Destroy(gameObject);
        }
        if (difficultyUpdatedFirstTime == false)
        {
            difficulty = 1;
            moneyMult = 1;
        }
    }
    public void UpdateDifficulty(int difficultyLevel)
    {
        difficultyUpdatedFirstTime = true;
        if (difficultyLevel == 0) moneyMult = easyModeMoneyMult;
        else if (difficultyLevel == 2) moneyMult = hardModeMoneyMult;
        else moneyMult = 1;
    }
}
