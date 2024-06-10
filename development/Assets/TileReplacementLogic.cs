using System.Collections.Generic;
using UnityEngine;

public class TileReplacementLogic : MonoBehaviour
{
    private GridManager gridManager;

    void Start()
    {
        gridManager = GetComponent<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("GridManager component is missing from the TileReplacementLogic GameObject.");
        }
    }

    public void ReplaceTiles()
    {
        for (int x = 0; x < gridManager.width; x++)
        {
            for (int y = 0; y < gridManager.height; y++)
            {
                if (gridManager.grid[x, y] == null)
                {
                    for (int i = y; i < gridManager.height - 1; i++)
                    {
                        gridManager.grid[x, i] = gridManager.grid[x, i + 1];
                        if (gridManager.grid[x, i] != null)
                        {
                            gridManager.grid[x, i].transform.position = new Vector3(x, i, 0);
                        }
                    }
                    gridManager.grid[x, gridManager.height - 1] = Instantiate(gridManager.tilePrefab, new Vector3(x, gridManager.height - 1, 0), Quaternion.identity).GetComponent<Tile>();
                    gridManager.grid[x, gridManager.height - 1].Initialize(new Color(Random.value, Random.value, Random.value), x, gridManager.height - 1);
                }
            }
        }
    }
}
