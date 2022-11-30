using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScene : MonoBehaviour
{
    public static MultiScene multiScene;

    [HideInInspector] public int difficulty;

    [HideInInspector] public float highScore;
    [HideInInspector] public float money;
    [SerializeField] private float raha;    // debuggaukseen
    [SerializeField] private float easyModeMoneyMult;
    [SerializeField] private float hardModeMoneyMult;
    [HideInInspector] public float moneyMult;
    private bool difficultyUpdatedFirstTime;

    public List<GameObject> purchasedCards;
    public List<GameObject> cardsOnDeck;
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
        if (SaveManager.Instance.gameStartedFirstTime == false)      // t‰m‰ pit‰‰ kattoa uudestaan sit kun on save file script
        {
            money = 300;
            SaveManager.Instance.gameStartedFirstTime = true;
        }
    }
    private void Update()
    {
        raha = money;   // debuggaukseen
    }
    public void UpdateDifficulty(int difficultyLevel)
    {
        difficultyUpdatedFirstTime = true;
        if (difficultyLevel == 0) moneyMult = easyModeMoneyMult;
        else if (difficultyLevel == 2) moneyMult = hardModeMoneyMult;
        else moneyMult = 1;
    }
}
