using UnityEngine;

public class SplashController : MonoBehaviour
{
    void Start()
    {
        Invoke("GoToMenu", 5f); // wait 5seconds
    }

    void GoToMenu()
    {
        MainController.instance.LoadMainMenu();
    }
}