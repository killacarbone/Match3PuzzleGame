using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Adjust position to ensure tiles are spaced out properly
                Vector3 position = new Vector3(x, y, 0);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                // Change the color of the tile based on its position to differentiate them
                tile.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }

}
