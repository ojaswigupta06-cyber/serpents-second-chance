using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerCard : MonoBehaviour
{
    public static GameManagerCard Instance;

    public card cardPrefab;
    public Sprite[] cardFaces;
    public Sprite cardBack;

    public Transform cardHolder;
    public GameObject finalUI;
    public TextMeshProUGUI finalText;
    public TextMeshProUGUI timerText;

    // 🔊 ADD THESE (sounds)
    public AudioSource gameOverSound;
    public AudioSource winSound;

    // Private variables
    private List<card> cards;
    private List<int> cardIDs;
    public card firstCard;
    public card secondCard;
    private int pairsMatched;
    private int totalPairs;
    private float timer;
    private bool isGameOver;
    private bool isLevelFinished;

    public float maxTime = 60f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (cardPrefab == null || cardHolder == null)
        {
            Debug.LogError("CardPrefab or CardHolder not assigned!");
            return;
        }

        cards = new List<card>();
        cardIDs = new List<int>();

        pairsMatched = 0;
        totalPairs = cardFaces.Length / 2;
        timer = maxTime;
        isGameOver = false;
        isLevelFinished = false;

        CreateCards();
        ShuffleCards();

        if (finalUI != null)
            finalUI.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver && !isLevelFinished)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                GameOver();
            }
        }
    }

    void CreateCards()
    {
        for (int i = 0; i < cardFaces.Length / 2; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        foreach (int id in cardIDs)
        {
            card newCard = Instantiate(cardPrefab, cardHolder);
            newCard.gameManager = this;
            newCard.cardID = id;
            cards.Add(newCard);
        }
    }

    void ShuffleCards()
    {
        for (int i = 0; i < cardIDs.Count; i++)
        {
            int randomIndex = Random.Range(i, cardIDs.Count);
            int temp = cardIDs[i];
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].cardID = cardIDs[i];
        }
    }

    public void CardFlipped(card flippedCard)
    {
        if (firstCard == null)
        {
            firstCard = flippedCard;
        }
        else if (secondCard == null)
        {
            secondCard = flippedCard;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard.cardID == secondCard.cardID)
        {
            pairsMatched++;

            if (pairsMatched == totalPairs)
            {
                LevelFinished();
            }

            firstCard = null;
            secondCard = null;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstCard.HideCard();
            secondCard.HideCard();

            firstCard = null;
            secondCard = null;
        }
    }

    void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // 🔊 PLAY GAME OVER SOUND
        if (gameOverSound != null)
            gameOverSound.Play();

        FinalPanel();
        StartCoroutine(ReturnToBoard(false));
    }

    void LevelFinished()
    {
        if (isLevelFinished) return;

        isLevelFinished = true;

        // 🔊 PLAY WIN SOUND
        if (winSound != null)
            winSound.Play();

        FinalPanel();
        StartCoroutine(ReturnToBoard(true));
    }

    public void FinalPanel()
    {
        if (finalUI != null)
            finalUI.SetActive(true);

        if (isLevelFinished)
        {
            finalText.text = "You Won!\nTime left: " + Mathf.Round(timer) + "s";
        }
        else if (isGameOver)
        {
            finalText.text = "Game Over!\nTime's UP";
        }
    }

    IEnumerator ReturnToBoard(bool isWin)
    {
        yield return new WaitForSeconds(2.5f);

        if (MainController.instance != null)
        {
            if (isWin)
                MainController.instance.MinigameWin();
            else
                MainController.instance.MinigameLose();
        }
        else
        {
            Debug.LogError("MainController not found!");
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
            timerText.text = "Time: " + Mathf.Round(timer) + "s";
    }
}