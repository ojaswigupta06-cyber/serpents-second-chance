using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject popupPanel;
    public AudioSource clickSound; // 🔊 added

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popupPanel.activeSelf)
                ResumeGame();
            else
                OpenPopup();
        }
    }

    public void OpenPopup()
    {
        if (clickSound != null) clickSound.Play(); // 🔊 added
        popupPanel.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(AnimatePopup(Vector3.zero, Vector3.one));
    }

    public void ResumeGame()
    {
        if (clickSound != null) clickSound.Play(); // 🔊 added
        StartCoroutine(ClosePopup());
    }

    IEnumerator ClosePopup()
    {
        yield return AnimatePopup(Vector3.one, Vector3.zero);
        popupPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        if (clickSound != null) clickSound.Play(); // 🔊 added
        Time.timeScale = 1f;
        SceneManager.LoadScene("UIMainMenuScene");
    }

    IEnumerator AnimatePopup(Vector3 start, Vector3 end)
    {
        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            popupPanel.transform.localScale = Vector3.Lerp(start, end, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        popupPanel.transform.localScale = end;
    }
}