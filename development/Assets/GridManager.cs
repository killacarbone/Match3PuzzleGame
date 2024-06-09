using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    private GameObject[,] grid;
    private List<GameObject> matchedTiles = new List<GameObject>();



    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
                tile.AddComponent<TileInteraction>();
                grid[x, y] = tile;
            }
        }
    }

    public void OnTileClicked(GameObject tile)
    {
        Debug.Log("Tile clicked in GridManager: " + tile.name);
        Vector2Int tilePos = FindTilePosition(tile);
        List<GameObject> horizontalMatches = CheckMatches(tilePos.x, tilePos.y, Vector2Int.right);
        List<GameObject> verticalMatches = CheckMatches(tilePos.x, tilePos.y, Vector2Int.up);

        if (horizontalMatches.Count >= 3)
        {
            matchedTiles.AddRange(horizontalMatches);
        }

        if (verticalMatches.Count >= 3)
        {
            matchedTiles.AddRange(verticalMatches);
        }

        foreach (GameObject matchedTile in matchedTiles)
        {
            matchedTile.GetComponent<SpriteRenderer>().color = Color.white; // For example, mark matched tiles
        }
    }

    Vector2Int FindTilePosition(GameObject tile)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == tile)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return Vector2Int.zero; // Should not happen
    }

    List<GameObject> CheckMatches(int startX, int startY, Vector2Int direction)
    {
        List<GameObject> matches = new List<GameObject>();
        GameObject startTile = grid[startX, startY];
        SpriteRenderer startRenderer = startTile.GetComponent<SpriteRenderer>();

        if (startRenderer == null) return matches;

        Color startColor = startRenderer.color;
        int x = startX + direction.x;
        int y = startY + direction.y;

        while (x >= 0 && x < width && y >= 0 && y < height)
        {
            GameObject nextTile = grid[x, y];
            SpriteRenderer nextRenderer = nextTile.GetComponent<SpriteRenderer>();

            if (nextRenderer == null || nextRenderer.color != startColor)
            {
                break;
            }

            matches.Add(nextTile);
            x += direction.x;
            y += direction.y;
        }

        return matches;
    }

}
