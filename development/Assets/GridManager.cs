using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public Tile[,] grid; // Change this to Tile[,]
    private TileReplacementLogic tileReplacementLogic;
    private ScoringManager scoringManager;


    private List<Tile> matchedTiles = new List<Tile>();

    private Color[] colors = new Color[]
    {
        new Color(1f, 0f, 0f),   // Red
        new Color(1f, 0.5f, 0f), // Orange
        new Color(1f, 1f, 0f),   // Yellow
        new Color(0f, 1f, 0f),   // Green
        new Color(0f, 0f, 1f),   // Blue
        new Color(0.29f, 0f, 0.51f), // Indigo
        new Color(0.56f, 0f, 1f)  // Violet
    };


    void Start()
    {
        Debug.Log("GridManager Start Method Called");

        grid = new Tile[width, height];
        Debug.Log($"Grid initialized with width: {width}, height: {height}");

        Debug.Log($"Colors array length in Start: {colors.Length}"); // Add this line

        tileReplacementLogic = GetComponent<TileReplacementLogic>();
        scoringManager = GetComponent<ScoringManager>();

        if (tileReplacementLogic == null)
        {
            Debug.LogError("TileReplacementLogic component is missing from the GridManager GameObject.");
        }
        if (scoringManager == null)
        {
            Debug.LogError("ScoringManager component is missing from the GridManager GameObject.");
        }

        GenerateGrid();
    }


    void GenerateGrid()
    {
        do
        {
            ClearGrid();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    Tile tile = Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<Tile>();

                    int colorIndex = Random.Range(0, colors.Length);
                    Debug.Log($"Colors array length: {colors.Length}, Selected color index: {colorIndex}");
                    if (colorIndex < 0 || colorIndex >= colors.Length)
                    {
                        Debug.LogError($"Invalid color index {colorIndex} for colors array of length {colors.Length}");
                        return;
                    }

                    Color color = colors[colorIndex];
                    Debug.Log($"Assigning color index {colorIndex} at position ({x}, {y})");

                    tile.Initialize(color, x, y);
                    grid[x, y] = tile;
                }
            }
        } while (FindMatches().Count < 3);

        Debug.Log("Initial grid is valid with matches.");
    }


    void ClearGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                }
            }
        }
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


    public Color[] GetColors()
    {
        return colors;
    }


    private bool AreColorsEqual(Color color1, Color color2)
    {
        Debug.Log($"Comparing colors: {ColorToString(color1)} and {ColorToString(color2)}");
        return color1.Equals(color2);
    }


    private string ColorToString(Color color)
    {
        if (color == colors[0]) return "Red";
        if (color == colors[1]) return "Orange";
        if (color == colors[2]) return "Yellow";
        if (color == colors[3]) return "Green";
        if (color == colors[4]) return "Blue";
        if (color == colors[5]) return "Indigo";
        if (color == colors[6]) return "Violet";
        return "Unknown";
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


    public IEnumerator SwapAndCheckMatches(Tile tile1, Tile tile2)
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
            // Call ClearMatches and ReplaceTiles
            ClearMatches(matches);
            tileReplacementLogic.ReplaceTiles(); // Call the replacement logic here
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

                if (tile1 != null && tile2 != null && tile3 != null &&
                AreColorsEqual(tile1.GetColor(), tile2.GetColor()) &&
                AreColorsEqual(tile2.GetColor(), tile3.GetColor()))

                {
                    Debug.Log($"Comparing colors: {ColorToString(tile1.GetColor())} and {ColorToString(tile2.GetColor())}");
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

                if (tile1 != null && tile2 != null && tile3 != null &&
                AreColorsEqual(tile1.GetColor(), tile2.GetColor()) &&
                AreColorsEqual(tile2.GetColor(), tile3.GetColor()))

                {
                    Debug.Log($"Comparing colors: {ColorToString(tile1.GetColor())} and {ColorToString(tile2.GetColor())}");
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
        if (matchedTiles == null)
        {
            Debug.LogError("matchedTiles list is null");
            return;
        }

        foreach (Tile tile in matchedTiles)
        {
            if (tile == null)
            {
                Debug.LogError("One of the matched tiles is null");
                continue;
            }

            Vector2Int position = tile.GetPosition();
            grid[position.x, position.y] = null;
            Destroy(tile.gameObject);
        }
        Debug.Log("Matches cleared");
        scoringManager.UpdateScore(matchedTiles.Count * 10); // Update score based on number of tiles cleared
        tileReplacementLogic.ReplaceTiles(); // Call the replacement logic here
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