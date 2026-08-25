using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UISplashController : MonoBehaviour
{
    public CanvasGroup UICanvasGroup;
    public AudioSource UIAudioSource;

    public float UIFadeDuration = 1f;
    public float UIDisplayTime = 1.5f;
    public float UIMaxVolume = 0.5f;

    void Start()
    {
        StartCoroutine(UIFadeSequence());
    }

    IEnumerator UIFadeSequence()
    {
        // Start with no sound
        UIAudioSource.volume = 0;
        UIAudioSource.Play();

        // Fade IN (visual + audio)
        yield return StartCoroutine(UIFadeIn());

        // Wait
        yield return new WaitForSeconds(UIDisplayTime);

        // Fade OUT (visual + audio)
        yield return StartCoroutine(UIFadeOut());

        // Load next scene
        SceneManager.LoadScene("UIMainMenuScene");
    }

    IEnumerator UIFadeIn()
    {
        float time = 0;

        while (time < UIFadeDuration)
        {
            time += Time.deltaTime;

            float t = time / UIFadeDuration;

            UICanvasGroup.alpha = Mathf.Lerp(0, 1, t);
            UIAudioSource.volume = Mathf.Lerp(0, UIMaxVolume, t);

            yield return null;
        }

        UICanvasGroup.alpha = 1;
        UIAudioSource.volume = UIMaxVolume;
    }

    IEnumerator UIFadeOut()
    {
        float time = 0;

        while (time < UIFadeDuration)
        {
            time += Time.deltaTime;

            float t = time / UIFadeDuration;

            UICanvasGroup.alpha = Mathf.Lerp(1, 0, t);
            UIAudioSource.volume = Mathf.Lerp(UIMaxVolume, 0, t);

            yield return null;
        }

        UICanvasGroup.alpha = 0;
        UIAudioSource.volume = 0;
    }
}