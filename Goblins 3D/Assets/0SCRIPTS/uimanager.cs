using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class uimanager : MonoBehaviour
{
    private enum State
    {
        MainMenu, Play
    }

    private State state;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject runTimeUi;
    [SerializeField] private TMP_Text currentRunScore;
    [SerializeField] private TMP_Text newHighScoreText;

    [SerializeField] private TMP_Text mainMenuHighScoreText;

    public Slider resourceBar;
    [SerializeField] private float resourcesPerS;
    [SerializeField] private int startResources;
    [SerializeField] private TMP_Text resourceNumber;

    [SerializeField] private GameObject[] cards;
    [SerializeField] private float refreshCoolDown;

    [SerializeField] private Button oneRefreshButton;
    [SerializeField] private Image oneRefButtonCD;
    [SerializeField] private Transform oneCardPlace;

    [SerializeField] private Button twoRefreshButton;
    [SerializeField] private Image twoRefButtonCD;
    [SerializeField] private Transform twoCardPlace;

    [SerializeField] private Button threeRefreshButton;
    [SerializeField] private Image threeRefButtonCD;
    [SerializeField] private Transform threeCardPlace;

    [SerializeField] private Button fourRefreshButton;
    [SerializeField] private Image fourRefButtonCD;
    [SerializeField] private Transform fourCardPlace;

    private float timer1;
    private float timer2;
    private float timer3;
    private float timer4;

    private GameObject card1;
    private GameObject card2;
    private GameObject card3;
    private GameObject card4;

    public TMP_Text timerText;
    [HideInInspector] public float currentTime;
    [HideInInspector] public bool isTiming;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            state = State.Play;

            isTiming = true;
            currentTime = 0;

            resourceBar.maxValue = 10;
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
            resourceBar.value = startResources;

            StartCards();
        }
        else MainMenuState();
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.Play:
                if (resourceBar.value <= 10)
                {
                    resourceBar.value += Time.deltaTime * resourcesPerS;
                }
                resourceNumber.text = resourceBar.value.ToString("0");

                Timers();

                if (isTiming == true)
                {
                    currentTime += Time.deltaTime;
                    SetTimerText();
                }
                break;
            case State.MainMenu:
                break;
        }
    }
    void MainMenuState()
    {
        state = State.MainMenu;
        mainMenuHighScoreText.text = "High Score: " + System.TimeSpan.FromSeconds(MultiScene.multiScene.highScore).ToString("mm\\:ss\\.f");
    }
    void SetTimerText()
    {
        timerText.text = System.TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss\\.f");
    }
    public void PauseMenuOnOff()
    {
        if (pauseMenu.activeSelf == false) pauseMenu.SetActive(true);
        else pauseMenu.SetActive(false);

    }
    public void GameOverMenu()
    {
        isTiming = false;
        currentRunScore.text = "Your time: " + System.TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss\\.f");
        if (MultiScene.multiScene.highScore < gamemanager.userInterface.currentTime) MultiScene.multiScene.highScore = gamemanager.userInterface.currentTime;
        if (currentTime == MultiScene.multiScene.highScore) newHighScoreText.enabled = true;
        gameOverMenu.SetActive(true);
    }
    public void DisableRunTimeUI()
    {
        runTimeUi.SetActive(false);
    }
    public void RefreshOne()
    {
        if (gamemanager.playercards.selectedCard == card1) gamemanager.playercards.ResetSelectedCard();
        Destroy(card1);
        oneRefreshButton.enabled = false;
        GameObject newCard = Instantiate(cards[Random.Range(0, cards.Length)], oneCardPlace.position, Quaternion.identity);
        newCard.transform.SetParent(oneCardPlace.gameObject.transform);
        card1 = newCard;
        timer1 = refreshCoolDown;
    }
    public void RefreshTwo()
    {
        if (gamemanager.playercards.selectedCard == card2) gamemanager.playercards.ResetSelectedCard();
        Destroy(card2);
        twoRefreshButton.enabled = false;
        GameObject newCard = Instantiate(cards[Random.Range(0, cards.Length)], twoCardPlace.position, Quaternion.identity);
        newCard.transform.SetParent(twoCardPlace.gameObject.transform);
        card2 = newCard;
        timer2 = refreshCoolDown;
    }
    public void RefreshThree()
    {
        if (gamemanager.playercards.selectedCard == card3) gamemanager.playercards.ResetSelectedCard();
        Destroy(card3);
        threeRefreshButton.enabled = false;
        GameObject newCard = Instantiate(cards[Random.Range(0, cards.Length)], threeCardPlace.position, Quaternion.identity);
        newCard.transform.SetParent(threeCardPlace.gameObject.transform);
        card3 = newCard;
        timer3 = refreshCoolDown;
    }
    public void RefreshFour()
    {
        if (gamemanager.playercards.selectedCard == card4) gamemanager.playercards.ResetSelectedCard();
        Destroy(card4);
        fourRefreshButton.enabled = false;
        GameObject newCard = Instantiate(cards[Random.Range(0, cards.Length)], fourCardPlace.position, Quaternion.identity);
        newCard.transform.SetParent(fourCardPlace.gameObject.transform);
        card4 = newCard;
        timer4 = refreshCoolDown;
    }
    void StartCards()
    {
        SpawnCardOne();
        SpawnCardTwo();
        SpawnCardThree();
        SpawnCardFour();
    }
    public void SpawnCardOne()
    {
        GameObject newCard1 = Instantiate(cards[Random.Range(0, cards.Length)], oneCardPlace.position, Quaternion.identity);
        newCard1.transform.SetParent(oneCardPlace.gameObject.transform);
        card1 = newCard1;
    }
    public void SpawnCardTwo()
    {
        GameObject newCard2 = Instantiate(cards[Random.Range(0, cards.Length)], twoCardPlace.position, Quaternion.identity);
        newCard2.transform.SetParent(twoCardPlace.gameObject.transform);
        card2 = newCard2;
    }
    public void SpawnCardThree()
    {
        GameObject newCard3 = Instantiate(cards[Random.Range(0, cards.Length)], threeCardPlace.position, Quaternion.identity);
        newCard3.transform.SetParent(threeCardPlace.gameObject.transform);
        card3 = newCard3;
    }
    public void SpawnCardFour()
    {
        GameObject newCard4 = Instantiate(cards[Random.Range(0, cards.Length)], fourCardPlace.position, Quaternion.identity);
        newCard4.transform.SetParent(fourCardPlace.gameObject.transform);
        card4 = newCard4;
    }
    void Timers()
    {
        if (timer1 >= 0)
        {
            timer1 -= Time.deltaTime;
            oneRefButtonCD.fillAmount = timer1 / refreshCoolDown;
        }
        if (timer1 <= 0) oneRefreshButton.enabled = true;

        if (timer2 >= 0)
        {
            timer2 -= Time.deltaTime;
            twoRefButtonCD.fillAmount = timer2 / refreshCoolDown;
        }
        if (timer2 <= 0) twoRefreshButton.enabled = true;

        if (timer3 >= 0)
        {
            timer3 -= Time.deltaTime;
            threeRefButtonCD.fillAmount = timer3 / refreshCoolDown;
        }
        if (timer3 <= 0) threeRefreshButton.enabled = true;

        if (timer4 >= 0)
        {
            timer4 -= Time.deltaTime;
            fourRefButtonCD.fillAmount = timer4 / refreshCoolDown;
        }
        if (timer4 <= 0) fourRefreshButton.enabled = true;
    }
}
