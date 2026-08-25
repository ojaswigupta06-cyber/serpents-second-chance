using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip bgMusic;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip crashSound;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayMusic();
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

    public void PlayCrash()
    {
        sfxSource.PlayOneShot(crashSound);
    }

    // 🔥 DEBUG KEYS (optional)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            PlayCrash();

        if (Input.GetKeyDown(KeyCode.S))
            PlayLose();

        if (Input.GetKeyDown(KeyCode.D))
            PlayWin();
    }
}