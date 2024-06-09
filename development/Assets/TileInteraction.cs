using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private static GameObject firstTile;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (firstTile == null)
        {
            // First tile clicked
            firstTile = gameObject;
            Debug.Log("First tile selected: " + gameObject.name);
        }
        else
        {
            // Second tile clicked
            Debug.Log("Second tile selected: " + gameObject.name);
            SwapTiles(firstTile, gameObject);
            firstTile = null;
        }
    }

    void SwapTiles(GameObject tile1, GameObject tile2)
    {
        // Swap the positions of the two tiles
        Vector3 tempPosition = tile1.transform.position;
        tile1.transform.position = tile2.transform.position;
        tile2.transform.position = tempPosition;

        Debug.Log("Tiles swapped: " + tile1.name + " and " + tile2.name);
    }

}