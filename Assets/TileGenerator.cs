using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;

    public Transform board;     // Assign your board here
    public Transform[] tiles;

    [Header("Adjust these to fit grid perfectly")]
    public float paddingX = 1.0f;   // 🔥 tweak this
    public float paddingY = 1.0f;   // 🔥 tweak this

    void Start()
    {
        if (board == null)
        {
            Debug.LogError("❌ Board not assigned in TileGenerator!");
            return;
        }

        GenerateTiles();
    }

    void GenerateTiles()
    {
        tiles = new Transform[rows * columns];

        SpriteRenderer sr = board.GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError("❌ Board has no SpriteRenderer!");
            return;
        }

        // ✅ USE CENTER (fix offset issue)
        Vector3 boardCenter = sr.bounds.center;

        // ✅ REMOVE EXTRA BORDER SPACE USING PADDING
        float boardWidth = sr.bounds.size.x - paddingX;
        float boardHeight = sr.bounds.size.y - paddingY;

        float tileWidth = boardWidth / columns;
        float tileHeight = boardHeight / rows;

        float startX = boardCenter.x - boardWidth / 2 + tileWidth / 2;
        float startY = boardCenter.y - boardHeight / 2 + tileHeight / 2;

        int index = 0;

        for (int y = 0; y < rows; y++)
        {
            if (y % 2 == 0)
            {
                for (int x = 0; x < columns; x++)
                {
                    CreateTile(x, y, startX, startY, tileWidth, tileHeight, ref index);
                }
            }
            else
            {
                for (int x = columns - 1; x >= 0; x--)
                {
                    CreateTile(x, y, startX, startY, tileWidth, tileHeight, ref index);
                }
            }
        }

        Debug.Log("✅ Tiles Generated: " + tiles.Length);
    }

    void CreateTile(int x, int y, float startX, float startY, float tileWidth, float tileHeight, ref int index)
    {
        GameObject tile = new GameObject("Tile_" + index);

        // ✅ IMPORTANT: parent to board (not generator)
        tile.transform.parent = board;

        float posX = startX + x * tileWidth;
        float posY = startY + y * tileHeight;

        tile.transform.position = new Vector3(posX, posY, 0);

        tiles[index] = tile.transform;

        index++;
    }
}