using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 8;
    public int columns = 8;
    public GameObject tilePrefab;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(column, row, 0), Quaternion.identity);
                tile.transform.parent = this.transform;
                tile.name = "Tile_" + row + "_" + column;

                // Add BoxCollider2D component
                if (tile.GetComponent<BoxCollider2D>() == null)
                {
                    tile.AddComponent<BoxCollider2D>();
                }
            }
        }
    }
}
