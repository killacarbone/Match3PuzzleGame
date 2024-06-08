using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " was clicked!");
    }
}
