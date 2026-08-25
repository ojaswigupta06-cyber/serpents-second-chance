using UnityEngine;

public class UIButtonAudio : MonoBehaviour
{
    public AudioSource UISFXSource;
    public AudioClip UIClickSound;
    public float UIClickVolume = 1f;

    public void UIPlayClickSound()
    {
        UISFXSource.PlayOneShot(UIClickSound, UIClickVolume);
    }
}