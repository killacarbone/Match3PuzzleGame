using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public Tile[,] grid; // Change this to Tile[,]

    private List<Tile> matchedTiles = new List<Tile>();

    void Start()
    {
        grid = new Tile[width, height];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                Tile tile = Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<Tile>();
                tile.Initialize(new Color(Random.value, Random.value, Random.value), x, y);
                grid[x, y] = tile;
            }
        }

        // Temporary solution for visual confirmation of matches
        grid[0, 0].SetColor(Color.red);
        grid[1, 0].SetColor(Color.red);
        grid[2, 0].SetColor(Color.red);

        grid[0, 1].SetColor(Color.green);
        grid[0, 2].SetColor(Color.green);
        grid[0, 3].SetColor(Color.green);
    }

    public void OnTileClicked(Tile tile)
    {
        Debug.Log("Tile clicked in GridManager: " + tile.name);
        Vector2Int tilePos = tile.GetPosition();
        List<Tile> horizontalMatches = CheckMatches(tilePos.x, tilePos.y, Vector2Int.right);
        List<Tile> verticalMatches = CheckMatches(tilePos.x, tilePos.y, Vector2Int.up);

        if (horizontalMatches.Count >= 3)
        {
            matchedTiles.AddRange(horizontalMatches);
        }

        if (verticalMatches.Count >= 3)
        {
            matchedTiles.AddRange(verticalMatches);
        }

        foreach (Tile matchedTile in matchedTiles)
        {
            matchedTile.SetColor(Color.white); // For example, mark matched tiles
        }
    }

    List<Tile> CheckMatches(int startX, int startY, Vector2? direction = null)
    {
        List<Tile> matches = new List<Tile>();
        Tile startTile = grid[startX, startY];

        if (startTile == null) return matches;

        Color startColor = startTile.GetColor();
        Vector2[] directions = direction.HasValue ? new Vector2[] { direction.Value } : new Vector2[] { Vector2.right, Vector2.up };

        foreach (var dir in directions)
        {
            List<Tile> currentMatches = new List<Tile>();
            currentMatches.Add(startTile);

            int x = startX;
            int y = startY;

            while (true)
            {
                x += (int)dir.x;
                y += (int)dir.y;

                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    Tile nextTile = grid[x, y];

                    if (nextTile == null || nextTile.GetColor() != startColor)
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

    public IEnumerator<object> SwapAndCheckMatches(Tile tile1, Tile tile2)
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

        // Debug log for swapped positions
        Debug.Log($"Swapped: {tile1.name} with {tile2.name}");
        Debug.Log($"New Position - {tile1.name}: {tile1.transform.position}");
        Debug.Log($"New Position - {tile2.name}: {tile2.transform.position}");

        yield return new WaitForSeconds(0.5f); // Adding a short delay for visual effect

        // Check for matches from both tiles
        List<Tile> matches = new List<Tile>();
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

            Debug.Log("Swapped back as no matches were found.");
        }
        else
        {
            // Destroy matched tiles
            foreach (Tile match in matches)
            {
                Destroy(match.gameObject);
            }
            Debug.Log("Matches found and tiles destroyed.");
            // TODO: Implement grid refill and cascading matches
        }
    }

    public Vector2Int GetTilePosition(Tile tile)
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

    public List<Tile> FindMatches()
    {
        List<Tile> matchedTiles = new List<Tile>();

        // Check rows for matches
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width - 2; x++)
            {
                Tile tile1 = grid[x, y];
                Tile tile2 = grid[x + 1, y];
                Tile tile3 = grid[x + 2, y];

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
                Tile tile1 = grid[x, y];
                Tile tile2 = grid[x, y + 1];
                Tile tile3 = grid[x, y + 2];

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

        return matchedTiles;
    }

    public void ClearMatches(List<Tile> matchedTiles)
    {
        foreach (Tile tile in matchedTiles)
        {
            Vector2Int position = tile.GetPosition();
            grid[position.x, position.y] = null;
            Destroy(tile.gameObject);
        }
        Debug.Log("Matches cleared");
        RefillGrid();
        CheckForCascadingMatches();
    }

    void RefillGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == null)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    Tile tile = Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<Tile>();
                    tile.SetColor(new Color(Random.value, Random.value, Random.value));
                    grid[x, y] = tile;
                }
            }
        }
        Debug.Log("Grid refilled");
        CheckForCascadingMatches();
    }

    void CheckForCascadingMatches()
    {
        while (true)
        {
            List<Tile> newMatches = FindMatches();
            if (newMatches.Count == 0)
            {
                break;
            }
            ClearMatches(newMatches);
        }
    }
}