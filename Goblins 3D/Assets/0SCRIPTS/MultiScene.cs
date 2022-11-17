using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScene : MonoBehaviour
{
    public static MultiScene multiScene;

    public int difficulty = 1;

    [HideInInspector] public float highScore;
    [HideInInspector] public float money;
    [SerializeField] private float easyModeMoneyMult;
    [SerializeField] private float hardModeMoneyMult;
    [HideInInspector] public float moneyMult;
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
    }
    private void Start()
    {
        if (difficulty == 0) moneyMult = easyModeMoneyMult;
        else if (difficulty == 2) moneyMult = hardModeMoneyMult;
        else moneyMult = 1;
    }
}
