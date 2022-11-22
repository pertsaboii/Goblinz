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
    private Card card;

    [Header("Name Panel")]
    [SerializeField] private string unitName;
    [SerializeField] private Button infoPanelButton;
    [SerializeField] private string infoPanelText;
    [SerializeField] private VideoClip infoPanelVideo;

    [Header("Purchased")]
    [SerializeField] private Image selectedPanel;
    [SerializeField] private TMP_Text purResCostText;
    [SerializeField] private Image purchasedImage;
    [SerializeField] private Button toDeckButton;
    [SerializeField] private Color32 notInDeckColor;
    [SerializeField] private Color32 inDeckColor;
    [SerializeField] private TMP_Text deckButtonText;
    [SerializeField] private Image resCircleColor;

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
        card = cardPrefab.GetComponent<Card>();
        resCircleColor.color = card.costCircle.color;
        infoPanelButton.onClick.AddListener(OpenInfoPanel);
        if (MultiScene.multiScene.purchasedCards.Contains(cardPrefab)) PurchasedState();
        else NotPurchasedState();
    }
    void NotPurchasedState()
    {
        state = State.NotPurchased;
        selectedPanel.transform.localScale = Vector3.zero;
        purResCostText.text = card.cost.ToString();
        notPurResCostText.text = card.cost.ToString();
        purchaseCostText.text = cardCost.ToString();
        purchaseButton.onClick.AddListener(PurchaseCard);
        // kun valmiit kuvat niin tänne koodi joka hakee kortin kuvat prefabista
    }
    void PurchasedState()
    {
        if (MultiScene.multiScene.cardsOnDeck.Contains(cardPrefab))
        {
            toDeckButton.image.color = inDeckColor;
            deckButtonText.text = "Remove";
        }
        else
        {
            toDeckButton.image.color = notInDeckColor;
            deckButtonText.text = "Add To Deck";
        }
        purResCostText.text = card.cost.ToString();
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
            CardPop();
            MultiScene.multiScene.money -= cardCost;
            SoundManager.Instance.PlayUISound(gamemanager.assetBank.FindSound(AssetBank.Sound.MoneyGained));
            gamemanager.userInterface.deckTabMoneyText.text = MultiScene.multiScene.money.ToString();
            gamemanager.userInterface.deckTabMoneyText.rectTransform.DOPunchScale(Vector3.one * -0.3f, 0.25f, 5, 1f);
            MultiScene.multiScene.purchasedCards.Add(cardPrefab);
            PurchasedState();
        }
    }
    public void CardOnOffDeck()
    {
        CardPop();
        SoundManager.Instance.PlayUISound(gamemanager.assetBank.FindSound(AssetBank.Sound.CardSelected));

        if (MultiScene.multiScene.cardsOnDeck.Contains(cardPrefab))
        {
            selectedPanel.transform.DOScale(Vector3.zero, .1f).SetEase(Ease.InSine);
            MultiScene.multiScene.cardsOnDeck.Remove(cardPrefab);
            toDeckButton.image.color = notInDeckColor;
            deckButtonText.text = "Add To Deck";
        }
        else
        {
            SoundManager.Instance.PlayUISound(gamemanager.assetBank.FindSound(AssetBank.Sound.CardSelected));
            selectedPanel.transform.DOScale(Vector3.one, .1f);
            MultiScene.multiScene.cardsOnDeck.Add(cardPrefab);
            toDeckButton.image.color = inDeckColor;
            deckButtonText.text = "Remove";
        }
    }
    public void OpenInfoPanel()
    {
        CardPop();
        gamemanager.userInterface.infoPanel.InfoPanelOn(unitName, infoPanelText, infoPanelVideo);
    }
    void CardPop()
    {
        gameObject.transform.DOPunchScale(transform.localScale * .1f, .15f, 5, 0.1f);
    }
}
