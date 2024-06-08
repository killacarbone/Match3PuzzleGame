using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;
    public GameObject tilePrefab;
    private Tile[,] grid;

    void Start()
    {
        grid = new Tile[rows, columns];
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector3(col, row, 0), Quaternion.identity);
                newTile.transform.parent = transform;
                grid[row, col] = newTile.GetComponent<Tile>();
            }
        }
    }

    public void DetectMatches()
    {
        List<Tile> matchedTiles = new List<Tile>();

        // Horizontal matches
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns - 2; col++)
            {
                if (grid[row, col] != null && grid[row, col + 1] != null && grid[row, col + 2] != null)
                {
                    if (grid[row, col].CompareTag(grid[row, col + 1].tag) && grid[row, col].CompareTag(grid[row, col + 2].tag))
                    {
                        matchedTiles.Add(grid[row, col]);
                        matchedTiles.Add(grid[row, col + 1]);
                        matchedTiles.Add(grid[row, col + 2]);
                    }
                }
            }
        }

        // Vertical matches
        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows - 2; row++)
            {
                if (grid[row, col] != null && grid[row + 1, col] != null && grid[row + 2, col] != null)
                {
                    if (grid[row, col].CompareTag(grid[row + 1, col].tag) && grid[row, col].CompareTag(grid[row + 2, col].tag))
                    {
                        matchedTiles.Add(grid[row, col]);
                        matchedTiles.Add(grid[row + 1, col]);
                        matchedTiles.Add(grid[row + 2, col]);
                    }
                }
            }
        }

        if (matchedTiles.Count > 0)
        {
            foreach (Tile tile in matchedTiles)
            {
                Destroy(tile.gameObject);
            }
            // Refill grid
        }
    }
}
