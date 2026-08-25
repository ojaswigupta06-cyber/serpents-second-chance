using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform[] tiles = new Transform[100];

    void Awake()
    {
        tiles = new Transform[100];

        int index = 0;

        // Automatically get tile positions from children
        foreach (Transform child in transform)
        {
            tiles[index] = child;
            index++;
        }
    }
}