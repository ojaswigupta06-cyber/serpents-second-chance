using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] tiles;
    public int currentTile = 0;

    public float moveSpeed = 5f;
    public bool isMoving = false;

    void Start()
    {
        StartCoroutine(SetupPlayer());
    }

    IEnumerator SetupPlayer()
    {
        yield return new WaitForSeconds(0.2f);

        TileGenerator generator = FindObjectOfType<TileGenerator>();

        if (generator == null)
        {
            Debug.LogError("❌ TileGenerator not found!");
            yield break;
        }

        tiles = generator.tiles;

        if (tiles == null || tiles.Length == 0)
        {
            Debug.LogError("❌ Tiles not generated!");
            yield break;
        }

        Debug.Log("✅ Total Tiles: " + tiles.Length);

        // 🔥 FIX: RETURN TO CORRECT TILE
        if (PlayerPrefs.HasKey("ReturnTile"))
        {
            currentTile = PlayerPrefs.GetInt("ReturnTile");
            PlayerPrefs.DeleteKey("ReturnTile");
        }
        else
        {
            currentTile = 0;
        }

        transform.position = tiles[currentTile].position;
    }

    public void Move(int steps)
    {
        if (isMoving) return;

        if (currentTile + steps > tiles.Length - 1)
        {
            Debug.Log("❌ Need exact number to win!");
            return;
        }

        StartCoroutine(MoveSteps(steps));
    }

    IEnumerator MoveSteps(int steps)
    {
        isMoving = true;

        for (int i = 0; i < steps; i++)
        {
            if (currentTile >= tiles.Length - 1)
                break;

            currentTile++;

            Vector3 target = tiles[currentTile].position;

            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    moveSpeed * Time.deltaTime
                );

                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.CheckLadderOrSnake(this);
        }

        isMoving = false;
    }
}