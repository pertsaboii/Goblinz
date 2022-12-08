using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;   //mahdollistaa binary tiedostojen tekemisen eli datan varastoinnin
using System.IO;                                        //(input output)

public class MultiScene : MonoBehaviour
{
    public static MultiScene multiScene;

    [HideInInspector] public int difficulty;

    // savedata
    public float highScore;
    public float money;
    public string cardIDs;
    public string deckCards;
    [SerializeField] private bool gameStartedFirstTime;

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

        Load();
    }
    public void UpdateDifficulty(int difficultyLevel)
    {
        difficultyUpdatedFirstTime = true;
        if (difficultyLevel == 0) moneyMult = easyModeMoneyMult;
        else if (difficultyLevel == 2) moneyMult = hardModeMoneyMult;
        else moneyMult = 1;
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()  //tiedon tallennus
    {
        BinaryFormatter bf = new BinaryFormatter(); //tekee uuden binaryformatterin eli sen joka kirjoittaa datan
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); //tekee tiedoston, xxx.dat <- luotavan tiedoston nimi

        PlayerData data = new PlayerData(); //hakee tiedostoon tallennettavat tiedot
        data.highScore = highScore;
        data.money = money;
        data.gameStartedFirstTime = gameStartedFirstTime;
        data.cardIDs = cardIDs;
        data.deckCards = deckCards;

        bf.Serialize(file, data);   // tallentaa tiedot tiedostoon
        file.Close();   //sulkee tiedoston
        Debug.Log("game saved");
    }

    public void Load()  //avaa olemassaolevan tiedoston
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))     //tarkistaa että tiedosto on olemassa
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); //otetaan data tiedostosta
            file.Close();   //sulkee tiedoston

            highScore = data.highScore;
            money = data.money;
            gameStartedFirstTime = data.gameStartedFirstTime;
            if (data.cardIDs != null) cardIDs = data.cardIDs;
            if (data.deckCards != null) deckCards = data.deckCards;
            Debug.Log("game loaded");
            Debug.Log(data.gameStartedFirstTime);
        }
        if (gameStartedFirstTime == false)
        {
            money = 300;
            gameStartedFirstTime = true;
        }
    }
    public void ResetSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
        money = 300;
        highScore = 0;
        gameStartedFirstTime = true;
        foreach (GameObject deckTabCard in gamemanager.userInterface.deckTabCards)
        {
            ShopCard shopCardScript = deckTabCard.transform.GetChild(0).GetComponent<ShopCard>();
            if (cardIDs.Contains(shopCardScript.cardID)) shopCardScript.NotPurchasedState();
        }
        cardIDs = "";
        deckCards = "";
        cardsOnDeck.Clear();
        purchasedCards.Clear();
        gamemanager.userInterface.mainMenuHighScoreText.text = "High Score: " + MultiScene.multiScene.highScore.ToString();
        gamemanager.userInterface.deckTabMoneyText.text = MultiScene.multiScene.money.ToString();
        SoundManager.Instance.PlayUISound(gamemanager.assetBank.FindSound(AssetBank.Sound.ButtonClicked));
        Debug.Log("progress reset");
    }
}

[Serializable]      //mahdollistaa asioiden tallentamisen tiedostoon
class PlayerData    //asiat joita tallennetaan ja lueataan save filestä, pohja näille
{
    public float highScore;
    public float money;
    public bool gameStartedFirstTime;
    public string cardIDs = "";
    public string deckCards = "";
}
