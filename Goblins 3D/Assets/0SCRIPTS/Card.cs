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
    }
}
