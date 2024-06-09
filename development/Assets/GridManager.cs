using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[,] grid; // Make this public

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

    List<GameObject> CheckMatches(int startX, int startY, Vector2? direction = null)
    {
        List<GameObject> matches = new List<GameObject>();
        GameObject startTile = grid[startX, startY];
        SpriteRenderer startRenderer = startTile.GetComponent<SpriteRenderer>();

        if (startRenderer == null) return matches;

        Color startColor = startRenderer.color;
        Vector2[] directions = direction.HasValue ? new Vector2[] { direction.Value } : new Vector2[] { Vector2.right, Vector2.up };

        foreach (var dir in directions)
        {
            List<GameObject> currentMatches = new List<GameObject>();
            currentMatches.Add(startTile);

            int x = startX;
            int y = startY;

            while (true)
            {
                x += (int)dir.x;
                y += (int)dir.y;

                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    GameObject nextTile = grid[x, y];
                    SpriteRenderer nextRenderer = nextTile.GetComponent<SpriteRenderer>();

                    if (nextRenderer == null || nextRenderer.color != startColor)
                    {
                        break;
                    }

                    currentMatches.Add(nextTile);
                }
                else
                {
                    break;
                }
            }

            if (currentMatches.Count >= 3)
            {
                matches.AddRange(currentMatches);
            }
        }
        return matches;
    }

    public void SwapAndCheckMatches(GameObject tile1, GameObject tile2)
    {
        // Swap positions of tile1 and tile2 in the grid
        Vector3 tempPosition = tile1.transform.position;
        tile1.transform.position = tile2.transform.position;
        tile2.transform.position = tempPosition;

        // Swap references in the grid array
        Vector2Int tile1Pos = GetTilePosition(tile1);
        Vector2Int tile2Pos = GetTilePosition(tile2);

        grid[tile1Pos.x, tile1Pos.y] = tile2;
        grid[tile2Pos.x, tile2Pos.y] = tile1;

        // Check for matches from both tiles
        List<GameObject> matches = new List<GameObject>();
        matches.AddRange(CheckMatches(tile1Pos.x, tile1Pos.y));
        matches.AddRange(CheckMatches(tile2Pos.x, tile2Pos.y));

        // If no matches, swap back
        if (matches.Count < 3)
        {
            // Swap back positions
            tempPosition = tile1.transform.position;
            tile1.transform.position = tile2.transform.position;
            tile2.transform.position = tempPosition;

            // Swap references back in the grid array
            grid[tile1Pos.x, tile1Pos.y] = tile1;
            grid[tile2Pos.x, tile2Pos.y] = tile2;
        }
        else
        {
            // Destroy matched tiles
            foreach (GameObject match in matches)
            {
                Destroy(match);
            }
        }
    }

    public Vector2Int GetTilePosition(GameObject tile)
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
        return Vector2Int.zero; // This should never happen
    }


    public void FindMatches()
    {
        List<GameObject> matchedTiles = new List<GameObject>();

        // Check rows for matches
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width - 2; x++)
            {
                GameObject tile1 = grid[x, y];
                GameObject tile2 = grid[x + 1, y];
                GameObject tile3 = grid[x + 2, y];

                if (tile1.GetComponent<SpriteRenderer>().color == tile2.GetComponent<SpriteRenderer>().color &&
                    tile2.GetComponent<SpriteRenderer>().color == tile3.GetComponent<SpriteRenderer>().color)
                {
                    if (!matchedTiles.Contains(tile1)) matchedTiles.Add(tile1);
                    if (!matchedTiles.Contains(tile2)) matchedTiles.Add(tile2);
                    if (!matchedTiles.Contains(tile3)) matchedTiles.Add(tile3);
                }
            }
        }

        // Check columns for matches
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height - 2; y++)
            {
                GameObject tile1 = grid[x, y];
                GameObject tile2 = grid[x, y + 1];
                GameObject tile3 = grid[x, y + 2];

                if (tile1.GetComponent<SpriteRenderer>().color == tile2.GetComponent<SpriteRenderer>().color &&
                    tile2.GetComponent<SpriteRenderer>().color == tile3.GetComponent<SpriteRenderer>().color)
                {
                    if (!matchedTiles.Contains(tile1)) matchedTiles.Add(tile1);
                    if (!matchedTiles.Contains(tile2)) matchedTiles.Add(tile2);
                    if (!matchedTiles.Contains(tile3)) matchedTiles.Add(tile3);
                }
            }
        }

        if (matchedTiles.Count > 0)
        {
            Debug.Log($"Matches found: {matchedTiles.Count}");
        }
        else
        {
            Debug.Log("No matches found.");
        }
    }


    public void RemoveMatches(List<GameObject> matchedTiles)
    {
        foreach (GameObject tile in matchedTiles)
        {
            Vector2Int pos = GetTilePosition(tile);
            grid[pos.x, pos.y] = null;
            Destroy(tile);
        }
    }


}
