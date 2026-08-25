using UnityEngine;

public class WinUI : MonoBehaviour
{
    public GameObject winScreen;

    public void ShowWin()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f; // pause game
    }
}