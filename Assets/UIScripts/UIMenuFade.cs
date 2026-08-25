using UnityEngine;
using System.Collections;

public class UIMenuFade : MonoBehaviour
{
    public CanvasGroup UICanvasGroup;
    public float UIFadeDuration = 1f;

    void Start()
    {
        StartCoroutine(UIFadeIn());
    }

    IEnumerator UIFadeIn()
    {
        float time = 0;

        while (time < UIFadeDuration)
        {
            time += Time.deltaTime;
            UICanvasGroup.alpha = Mathf.Lerp(0, 1, time / UIFadeDuration);
            yield return null;
        }

        UICanvasGroup.alpha = 1;
    }
}