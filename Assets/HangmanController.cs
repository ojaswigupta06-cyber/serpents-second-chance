using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HangmanController : MonoBehaviour
{
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject keyBoardContainer;
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] TextAsset possibleWord;
    [SerializeField] TextMeshProUGUI timerText;

    private float timeLeft = 45f;
    private bool isGameRunning = true;

    private string word;
    private int incorrectGuesses, correctGuesses;

    private List<TextMeshProUGUI> letterSlots = new List<TextMeshProUGUI>();

    void Start()
    {
        InitialiseButtons();
        InitialiseGame();

        // 🎵 START BACKGROUND MUSIC
        if (HAudioManager.Instance != null)
            HAudioManager.Instance.PlayMusic();
    }

    void Update()
    {
        if (!isGameRunning) return;

        HandleKeyboardInput();

        timeLeft -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Ceil(timeLeft):00}";

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            timerText.text = "0";
            LoseGame();
        }
    }

    void HandleKeyboardInput()
    {
        for (KeyCode key = KeyCode.A; key <= KeyCode.Z; key++)
        {
            if (Input.GetKeyDown(key))
            {
                string letter = key.ToString();

                CheckLetter(letter);
                DisableButton(letter);
            }
        }
    }

    void DisableButton(string letter)
    {
        foreach (Button btn in keyBoardContainer.GetComponentsInChildren<Button>())
        {
            if (btn.GetComponentInChildren<TextMeshProUGUI>().text == letter)
            {
                btn.interactable = false;
                break;
            }
        }
    }

    private void InitialiseButtons()
    {
        for (int i = 65; i <= 90; i++)
        {
            CreateButton(i);
        }
    }

    private void InitialiseGame()
    {
        timeLeft = 45f;
        isGameRunning = true;
        incorrectGuesses = 0;
        correctGuesses = 0;

        foreach (Button child in keyBoardContainer.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
        }

        foreach (Transform child in wordContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject stage in hangmanStages)
        {
            stage.SetActive(false);
        }

        letterSlots.Clear();

        word = generateWord().ToUpper();

        foreach (char letter in word)
        {
            var temp = Instantiate(letterContainer, wordContainer.transform);
            var txt = temp.GetComponentInChildren<TextMeshProUGUI>();

            txt.text = "_";
            letterSlots.Add(txt);
        }
    }

    private void CreateButton(int i)
    {
        GameObject temp = Instantiate(letterButton, keyBoardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();

        temp.GetComponent<Button>().onClick.AddListener(delegate
        {
            CheckLetter(((char)i).ToString());
            temp.GetComponent<Button>().interactable = false;
        });
    }

    private string generateWord()
    {
        string[] wordlist = possibleWord.text.Split('\n');
        string line = wordlist[Random.Range(0, wordlist.Length)];
        return line.Trim();
    }

    private void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;

        for (int i = 0; i < word.Length; i++)
        {
            if (inputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuesses++;
                letterSlots[i].text = inputLetter;
            }
        }

        if (!letterInWord)
        {
            incorrectGuesses++;

            if (incorrectGuesses - 1 < hangmanStages.Length)
                hangmanStages[incorrectGuesses - 1].SetActive(true);
        }

        CheckOutCome();
    }

    private void LoseGame()
    {
        if (!isGameRunning) return;

        isGameRunning = false;

        for (int i = 0; i < word.Length; i++)
        {
            letterSlots[i].color = Color.red;
            letterSlots[i].text = word[i].ToString();
        }

        // 🔊 STOP MUSIC + PLAY LOSE SOUND
        if (HAudioManager.Instance != null)
        {
            HAudioManager.Instance.StopMusic();
            HAudioManager.Instance.PlayLose();
        }

        Invoke("ReturnLose", 2f);
    }

    private void CheckOutCome()
    {
        if (correctGuesses == word.Length)
        {
            isGameRunning = false;

            for (int i = 0; i < word.Length; i++)
            {
                letterSlots[i].text = "<color=green>" + word[i] + "</color>";
            }

            // 🔊 STOP MUSIC + PLAY WIN SOUND
            if (HAudioManager.Instance != null)
            {
                HAudioManager.Instance.StopMusic();
                HAudioManager.Instance.PlayWin();
            }

            Invoke("ReturnWin", 2f);
        }

        if (incorrectGuesses == hangmanStages.Length)
        {
            isGameRunning = false;

            for (int i = 0; i < word.Length; i++)
            {
                letterSlots[i].text = "<color=red>" + word[i] + "</color>";
            }

            // 🔊 STOP MUSIC + PLAY LOSE SOUND
            if (HAudioManager.Instance != null)
            {
                HAudioManager.Instance.StopMusic();
                HAudioManager.Instance.PlayLose();
            }

            Invoke("ReturnLose", 2f);
        }
    }

    void ReturnWin()
    {
        MainController.instance.MinigameWin();
    }

    void ReturnLose()
    {
        MainController.instance.MinigameLose();
    }
}