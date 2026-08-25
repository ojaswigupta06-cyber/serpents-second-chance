using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Dice dice;
    public PlayerMovement player;

    public GameObject winPanel;
    public AudioSource winSound;
    public ParticleSystem fireworks;

    private bool isRolling = false;

    void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void RollAndMove()
    {
        if (isRolling || Time.timeScale == 0f)
            return;

        StartCoroutine(RollAndMoveRoutine());
    }

    IEnumerator RollAndMoveRoutine()
    {
        isRolling = true;

        // 🎲 Roll Dice
        dice.RollDice();

        // ⏳ Wait until dice stops
        yield return new WaitUntil(() => dice.isRolling == false);

        int value = dice.finalValue;

        // 🚶 Move Player
        player.Move(value);

        // ⏳ Wait for movement
        yield return new WaitForSeconds(value * 0.4f);

        // 🏁 Check Win
        if (player.currentTile == player.tiles.Length - 1)
        {
            WinGame();
        }

        isRolling = false;
    }

    // 🎉 WIN FUNCTION
    public void WinGame()
    {
        Debug.Log("🎉 PLAYER WINS!");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("❌ WinPanel not assigned!");
        }

        if (winSound != null)
            winSound.Play();

        if (fireworks != null)
            fireworks.Play();

        // 🔥 Start coroutine
        StartCoroutine(WinFlow());
    }

    // ⏳ WIN FLOW
    IEnumerator WinFlow()
    {
        // Pause game so panel is visible
        Time.timeScale = 0f;

        // Wait in real time (IMPORTANT)
        yield return new WaitForSecondsRealtime(5f);

        // Resume game
        Time.timeScale = 1f;

        // Go to Main Menu
        MainController.instance.LoadMainMenu();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}