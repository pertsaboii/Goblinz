using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Video;

public class ShopCard : MonoBehaviour
{
    private enum State
    {
        NotPurchased, Purchased
    }

    private State state;

    [Header("Name Panel")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private string unitName;
    [SerializeField] private Button infoPanelButton;
    [SerializeField] private string infoPanelText;
    [SerializeField] private VideoClip infoPanelVideo;

    [Header("Purchased")]
    [SerializeField] private Image selectedPanel;
    [SerializeField] private TMP_Text purResCostText;
    [SerializeField] private Image purchasedImage;
    [SerializeField] private Button toDeckButton;
    [Header("Not Purchased")]
    [SerializeField] private GameObject notPurchasedCard;
    [SerializeField] private TMP_Text notPurResCostText;
    [SerializeField] private TMP_Text purchaseCostText;
    [SerializeField] private Image notPurchasedImage;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private float cardCost;

    [Header("Card Prefab")]
    [SerializeField] private GameObject cardPrefab;
    void Start()
    {
        nameText.text = unitName;
        infoPanelButton.onClick.AddListener(OpenInfoPanel);
        if (MultiScene.multiScene.purchasedCards.Contains(cardPrefab)) PurchasedState();
        else NotPurchasedState();
    }
    void NotPurchasedState()
    {
        state = State.NotPurchased;
        selectedPanel.transform.localScale = Vector3.zero;
        purResCostText.text = cardPrefab.GetComponent<Card>().cost.ToString();
        notPurResCostText.text = cardPrefab.GetComponent<Card>().cost.ToString();
        purchaseCostText.text = cardCost.ToString();
        purchaseButton.onClick.AddListener(PurchaseCard);
        // kun valmiit kuvat niin tänne koodi joka hakee kortin kuvat prefabista
    }
    void PurchasedState()
    {
        purResCostText.text = cardPrefab.GetComponent<Card>().cost.ToString();
        toDeckButton.onClick.AddListener(CardOnOffDeck);
        // kun valmiit kuvat niin tänne koodi joka hakee kortin kuvat prefabista
        notPurchasedCard.SetActive(false);
        state = State.Purchased;
        if (MultiScene.multiScene.cardsOnDeck.Contains(cardPrefab)) selectedPanel.transform.localScale = Vector3.one;
        else selectedPanel.transform.localScale = Vector3.zero;
    }
    public void PurchaseCard()
    {
        if (MultiScene.multiScene.money >= cardCost)
        {
            MultiScene.multiScene.money -= cardCost;
            gamemanager.userInterface.deckTabMoneyText.text = MultiScene.multiScene.money.ToString();
            MultiScene.multiScene.purchasedCards.Add(cardPrefab);
            PurchasedState();
        }
    }
    public void CardOnOffDeck()
    {
        if (MultiScene.multiScene.cardsOnDeck.Contains(cardPrefab))
        {
            selectedPanel.transform.DOScale(Vector3.zero, .1f).SetEase(Ease.InSine);
            MultiScene.multiScene.cardsOnDeck.Remove(cardPrefab);
        }
        else
        {
            selectedPanel.transform.DOScale(Vector3.one, .1f);
            MultiScene.multiScene.cardsOnDeck.Add(cardPrefab);
        }
    }
    public void OpenInfoPanel()
    {
        gamemanager.userInterface.infoPanel.InfoPanelOn(unitName, infoPanelText, infoPanelVideo);
    }
}
