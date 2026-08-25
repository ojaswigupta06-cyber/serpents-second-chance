using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public void UIStartGame()
    {
        MainController.instance.StartGame(); // go to BoardScene
    }

    public void UIExitGame()
    {
        MainController.instance.ExitGame();
    }
}