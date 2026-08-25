using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public static MainController instance;

    public int currentTile;
    public int snakeTailTile;

    // 🔥 NEW: track if this minigame was triggered by snake
    private bool isSnakeEvent = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadStory(); // 🔥 Start from story now
    }

    // 🎬 STORY SCENE
    public void LoadStory()
    {
        SceneManager.LoadScene("StoryScene");
    }

    // ⏭ AFTER STORY → GAME TITLE
    public void LoadGameTitle()
    {
        SceneManager.LoadScene("UISplashScene");
    }

    // 🏠 MAIN MENU (WELCOME SCENE)
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("UIMainMenuScene");
    }

    // ▶ PLAY BUTTON
    public void StartGame()
    {
        SceneManager.LoadScene("BoardScene");
    }

    // ❌ EXIT BUTTON
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // 🎮 RANDOM MINIGAME (SNAKE EVENT)
    public void TriggerMinigame(int tile, int snakeTail)
    {
        currentTile = tile;
        snakeTailTile = snakeTail;

        isSnakeEvent = true; // 🔥 mark this as snake-triggered

        int random = Random.Range(0, 3);

        if (random == 0)
            SceneManager.LoadScene("HangmanScene");
        else if (random == 1)
            SceneManager.LoadScene("AvoidObjectsScene");
        else
            SceneManager.LoadScene("CardScene");
    }

    // ✅ MINIGAME WIN → STAY ON TILE
    public void MinigameWin()
    {
        Debug.Log("Minigame WIN");

        // 🔥 stay on same tile
        PlayerPrefs.SetInt("ReturnTile", currentTile);

        isSnakeEvent = false;

        SceneManager.LoadScene("BoardScene");
    }

    // ❌ MINIGAME LOSE → GO TO SNAKE TAIL
    public void MinigameLose()
    {
        Debug.Log("Minigame LOSE");

        if (isSnakeEvent)
        {
            // 🔥 go to snake tail ONLY if triggered by snake
            PlayerPrefs.SetInt("ReturnTile", snakeTailTile);
        }
        else
        {
            // fallback (just in case)
            PlayerPrefs.SetInt("ReturnTile", currentTile);
        }

        isSnakeEvent = false;

        SceneManager.LoadScene("BoardScene");
    }

    // 🏆 FINAL GAME WIN
    public void GameWon()
    {
        SceneManager.LoadScene("WinScene");
    }

    // 🔙 AFTER WIN PANEL → BACK TO MENU
    public void BackToMenu()
    {
        SceneManager.LoadScene("UIMainMenuScene");
    }

    // 🔄 GENERIC SCENE LOADER
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}