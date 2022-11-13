using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    public float cost;
    private string cardPlace;

    private void Start()
    {
        cardPlace = transform.parent.parent.name;
    }
    public void ButtonClick()
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
