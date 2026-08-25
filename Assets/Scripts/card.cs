using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public int cardID; // unique ID for this card

    public GameManagerCard gameManager; // reference to the gamemanager

    private bool isFlipped; // flag to check if the card is flipped

    public Image cardImage; // image component of the card

    // 🔊 ADD THIS (flip sound)
    public AudioSource flipSound;

    void Start()
    {
        isFlipped = false;

        // ✅ Safety check for GameManager
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManagerCard>();
            if (gameManager == null)
            {
                Debug.LogError("GameManagerCard NOT FOUND in scene!");
                return;
            }
        }

        // ✅ Safety check for Image
        if (cardImage == null)
        {
            cardImage = GetComponent<Image>();
            if (cardImage == null)
            {
                Debug.LogError("Card Image NOT assigned!");
                return;
            }
        }

        // 🔊 Auto assign AudioSource if not set
        if (flipSound == null)
        {
            flipSound = GetComponent<AudioSource>();
        }

        // ✅ Set card back safely
        if (gameManager.cardBack != null)
        {
            cardImage.sprite = gameManager.cardBack;
        }
        else
        {
            Debug.LogError("Card Back sprite missing in GameManagerCard!");
        }
    }

    public void FlipCard()
    {
        if (!isFlipped && (gameManager.firstCard == null || gameManager.secondCard == null))
        {
            isFlipped = true;

            // 🔊 PLAY SOUND HERE (IMPORTANT LINE)
            if (flipSound != null)
            {
                flipSound.pitch = Random.Range(0.9f, 1.1f); // nice variation
                flipSound.Play();
            }

            // ✅ Safety check
            if (gameManager.cardFaces != null && cardID < gameManager.cardFaces.Length)
            {
                cardImage.sprite = gameManager.cardFaces[cardID];
            }
            else
            {
                Debug.LogError("Card face missing or index out of range!");
            }

            gameManager.CardFlipped(this);
        }
    }

    // Method to hide the card's face
    public void HideCard()
    {
        isFlipped = false;

        if (gameManager.cardBack != null)
        {
            cardImage.sprite = gameManager.cardBack;
        }
    }
}