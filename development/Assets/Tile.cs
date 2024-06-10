using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color color;
    public Vector2Int position;  // Add position field
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        spriteRenderer.color = newColor;
    }

    public void Initialize(Color newColor, int x, int y)
    {
        SetColor(newColor);
        position = new Vector2Int(x, y);
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    public Color GetColor()
    {
        return color;
    }
}
