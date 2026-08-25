using UnityEngine;

public class HAudioManager : MonoBehaviour
{
    public static HAudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip bgMusic;
    public AudioClip winSound;
    public AudioClip loseSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlayMusic()
    {
        musicSource.clip = bgMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayWin()
    {
        sfxSource.PlayOneShot(winSound);
    }

    public void PlayLose()
    {
        sfxSource.PlayOneShot(loseSound);
    }
}