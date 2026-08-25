using UnityEngine;

public class RollButton : MonoBehaviour
{
    public void OnRollClick()
    {
        Debug.Log("🎯 Roll button pressed");

        GameController controller = FindObjectOfType<GameController>();

        if (controller != null)
        {
            controller.RollAndMove();
        }
        else
        {
            Debug.LogError("❌ GameController not found!");
        }
    }
}