using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private static Tile firstTile;
    private GridManager gridManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gridManager = FindObjectOfType<GridManager>();  // This line will find the GridManager instance in the scene
    }

    void OnMouseDown()
    {
        Tile tile = GetComponent<Tile>();
        if (firstTile == null)
        {
            // First tile clicked
            firstTile = tile;
            Debug.Log("First tile selected: " + gameObject.name);
        }
        else
        {
            // Second tile clicked
            Debug.Log("Second tile selected: " + gameObject.name);
            StartCoroutine(gridManager.SwapAndCheckMatches(firstTile, tile));  // Start the coroutine
            firstTile = null;
        }
    }

    public void SwapTiles(Tile tile1, Tile tile2)
    {
        Vector3 tempPosition = tile1.transform.position;
        tile1.transform.position = tile2.transform.position;
        tile2.transform.position = tempPosition;

        // Swap references in the grid
        Vector2Int tile1Pos = gridManager.GetTilePosition(tile1);
        Vector2Int tile2Pos = gridManager.GetTilePosition(tile2);

        gridManager.grid[tile1Pos.x, tile1Pos.y] = tile2;
        gridManager.grid[tile2Pos.x, tile2Pos.y] = tile1;

        Debug.Log($"Tiles swapped: {tile1.name} and {tile2.name}");

        gridManager.FindMatches();
    }

}