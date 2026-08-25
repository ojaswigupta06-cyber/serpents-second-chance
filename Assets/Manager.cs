using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public bool snakeTriggered = false;
    public int snakeTailPosition = 0;
    public int playerPositionBeforeMiniGame = 0;
    public bool miniGameResult = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}