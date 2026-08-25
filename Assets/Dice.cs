using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] diceSprites;

    // 🔊 SOUND SYSTEM
    public AudioSource audioSource;
    public AudioClip rollSound;

    public int finalValue;

    public float rollDuration = 1f;
    public float rollSpeed = 0.08f;

    public bool isRolling = false;

    public PlayerMovement player; // assign in Inspector

    void Awake()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void RollDice()
    {
        // ❌ Prevent spam clicking
        if (isRolling) return;

        // ❌ Prevent rolling while player is moving
        if (player != null && player.isMoving)
        {
            Debug.Log("⛔ Wait! Player is still moving.");
            return;
        }

        if (diceSprites == null || diceSprites.Length < 6)
        {
            Debug.LogError("❌ Assign all 6 dice sprites!");
            return;
        }

        StartCoroutine(RollAnimation());
    }

    IEnumerator RollAnimation()
    {
        isRolling = true;

        // 🔊 PLAY SOUND (BEST METHOD)
        if (audioSource != null && rollSound != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f); // 🎯 natural variation
            audioSource.PlayOneShot(rollSound);
        }

        float timer = 0f;

        // 🎲 Rolling animation
        while (timer < rollDuration)
        {
            int randomIndex = Random.Range(0, diceSprites.Length);
            sr.sprite = diceSprites[randomIndex];

            timer += rollSpeed;
            yield return new WaitForSeconds(rollSpeed);
        }

        // 🎯 Final result
        finalValue = Random.Range(1, 7);
        sr.sprite = diceSprites[finalValue - 1];

        Debug.Log("🎲 Final Dice Value: " + finalValue);

        isRolling = false;

        // 🚶 Move player
        if (player != null)
        {
            Debug.Log("🚶 Moving Player: " + finalValue + " steps");
            player.Move(finalValue);
        }
        else
        {
            Debug.LogError("❌ Player not assigned in Dice!");
        }
    }

    void OnMouseDown()
    {
        RollDice();
    }
}