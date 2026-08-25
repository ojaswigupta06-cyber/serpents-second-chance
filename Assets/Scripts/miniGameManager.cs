using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class miniGameManager : MonoBehaviour
{
    public float enemyY = 7;
    public GameObject[] enemy;
    private float xBoundary = 9.98f;
    public float spawnRate = 2;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;

    public bool gameRunning = true;

    public float winTime = 20f;
    private float survivalTimer = 0f;

    private bool gameEnded = false;

    void Start()
    {
        gameRunning = true;
        gameEnded = false;
        survivalTimer = 0f;
        score = 0;

        if (scoreText != null)
            scoreText.SetText("Score: 0");

        if (timerText != null)
            timerText.SetText("Time: " + winTime.ToString("F1") + "s");

        if (winText != null)
            winText.SetText("");

        if (loseText != null)
            loseText.SetText("");

        if (enemy == null || enemy.Length == 0)
        {
            Debug.LogError("❌ No enemies assigned!");
            return;
        }

        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnRate);

        // 🎵 Start background music
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic();
    }

    void Update()
    {
        if (!gameRunning || gameEnded) return;

        survivalTimer += Time.deltaTime;

        float remaining = Mathf.Max(0, winTime - survivalTimer);

        if (timerText != null)
            timerText.SetText("Time: " + remaining.ToString("F1") + "s");

        if (survivalTimer >= winTime)
        {
            WinGame();
        }
    }

    private void SpawnEnemy()
    {
        if (!gameRunning || gameEnded) return;

        float xPOS = Random.Range(-xBoundary, xBoundary);
        int index = Random.Range(0, enemy.Length);

        Instantiate(enemy[index], new Vector3(xPOS, enemyY), enemy[index].transform.rotation);
    }

    public void UpdateScore(int addend)
    {
        if (!gameRunning || gameEnded) return;

        score += addend;

        if (scoreText != null)
            scoreText.SetText("Score: " + score);
    }

    private void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        gameRunning = false;

        CancelInvoke(nameof(SpawnEnemy));
        DestroyAllEnemies();

        if (winText != null)
            winText.SetText("You Won!");

        if (timerText != null)
            timerText.SetText("Time: 0.0s");

        // 🔊 STOP MUSIC + PLAY WIN
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayWin();
        }

        StartCoroutine(ReturnWin());
    }

    public void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        gameRunning = false;

        CancelInvoke(nameof(SpawnEnemy));
        DestroyAllEnemies();

        if (loseText != null)
        {
            loseText.SetText("You Lost!\nYou survived for\n" + survivalTimer.ToString("F1") + " seconds");
        }

        // 🔊 STOP MUSIC + PLAY LOSE
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayLose();
        }

        StartCoroutine(ReturnLose());
    }

    void DestroyAllEnemies()
    {
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject e in remainingEnemies)
        {
            Destroy(e);
        }
    }

    IEnumerator ReturnWin()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("Calling MainController Win");

        if (MainController.instance != null)
        {
            MainController.instance.MinigameWin();
        }
        else
        {
            Debug.LogError("❌ MainController is NULL!");
        }
    }

    IEnumerator ReturnLose()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("Calling MainController Lose");

        if (MainController.instance != null)
        {
            MainController.instance.MinigameLose();
        }
        else
        {
            Debug.LogError("❌ MainController is NULL!");
        }
    }
}