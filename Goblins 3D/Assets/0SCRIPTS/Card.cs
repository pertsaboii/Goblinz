using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    public float cost;
    private string cardPlace;

    // nämä korvautuu mustavalkokuvalla myöhemmin
    [SerializeField] private Image insufFundsImage;
    [SerializeField] private Image costCircle;
    private Color32 originalCircleColor;

    private bool insufficientFunds;
    private void Start()
    {

        cardPlace = transform.parent.parent.name;
        originalCircleColor = costCircle.color;
    }
    public void ButtonClick()
    {
        if (insufficientFunds == false)
        {
            gamemanager.playercards.selectedCardUnit = unit;
            gamemanager.playercards.selectedCard = gameObject;
            gamemanager.playercards.selectedCardCost = cost;
            gamemanager.playercards.cardPlace = cardPlace;
            if (cardPlace == "1") gamemanager.userInterface.anim.SetInteger("CardSelected", 1);
            else if (cardPlace == "2") gamemanager.userInterface.anim.SetInteger("CardSelected", 2);
            else if (cardPlace == "3") gamemanager.userInterface.anim.SetInteger("CardSelected", 3);
            else if (cardPlace == "4") gamemanager.userInterface.anim.SetInteger("CardSelected", 4);
        }
    }
    private void Update()
    {
        if (gamemanager.userInterface.currentResources < cost && insufficientFunds == false)
        {
            insufficientFunds = true;
            insufFundsImage.enabled = true;
            costCircle.color = new Color32(104, 104, 104, 255);
        }
        else if (gamemanager.userInterface.currentResources >= cost && insufficientFunds == true)
        {
            insufficientFunds = false;
            insufFundsImage.enabled = false;
            costCircle.color = originalCircleColor;
        }
    }
}
