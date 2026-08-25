using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer UIVideoPlayer;
    public GameObject UISkip;

    void Start()
    {
        UIVideoPlayer.Play();

        // Hide skip initially (optional)
        UISkip.SetActive(false);
        Invoke("ShowSkip", 2f);

        UIVideoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Skip();
        }
    }

    void ShowSkip()
    {
        UISkip.SetActive(true);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadGame();
    }

    public void Skip()
    {
        UIVideoPlayer.Stop();
        LoadGame();
    }

    void LoadGame()
    {
        MainController.instance.LoadGameTitle();
    }
}