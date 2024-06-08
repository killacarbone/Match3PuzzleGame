using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2 initialPosition;
    private Vector2 finalPosition;

    private void OnMouseDown()
    {
        initialPosition = transform.position;
    }

    private void OnMouseUp()
    {
        finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(initialPosition, finalPosition) <= 1.1f) // Adjust distance for swapping
        {
            SwapTiles();
        }
    }

    private void SwapTiles()
    {
        Vector2 tempPosition = initialPosition;
        transform.position = finalPosition;
        // Add logic to update the grid array and the other tile's position
    }
}
