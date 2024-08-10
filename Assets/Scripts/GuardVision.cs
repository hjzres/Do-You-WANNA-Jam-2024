using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D), typeof(SpriteRenderer))]
public class GuardVision: MonoBehaviour
{
    void FixedUpdate()
    {
        // Get the PolygonCollider2D and SpriteRenderer components
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // Get the bounds of the PolygonCollider2D
        Bounds bounds = collider.bounds;

        // Calculate the width and height of the texture based on the bounds
        int width = Mathf.CeilToInt(bounds.size.x * 100);  // Increase size for better resolution
        int height = Mathf.CeilToInt(bounds.size.y * 100); // Increase size for better resolution
        Texture2D texture = new Texture2D(width, height);

        // Initialize the texture with a transparent background
        Color transparent = new Color(0, 0, 0, 0);
        Color[] fillColorArray = texture.GetPixels();
        for (int i = 0; i < fillColorArray.Length; i++)
        {
            fillColorArray[i] = transparent;
        }
        texture.SetPixels(fillColorArray);

        // Draw the polygon onto the texture
        Vector2[] points = collider.points;
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 start = (points[i] - (Vector2)bounds.min) * 100;  // Scale points to texture size and offset by bounds.min
            Vector2 end = (points[(i + 1) % points.Length] - (Vector2)bounds.min) * 100;  // Loop to the first point
            DrawLine(texture, start, end, Color.white);
        }

        // Apply the changes to the texture
        texture.Apply();

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // Assign the sprite to the SpriteRenderer
        renderer.sprite = sprite;
    }

    // Function to draw a line on the texture
    void DrawLine(Texture2D tex, Vector2 start, Vector2 end, Color color)
    {
        int x0 = Mathf.RoundToInt(start.x);
        int y0 = Mathf.RoundToInt(start.y);
        int x1 = Mathf.RoundToInt(end.x);
        int y1 = Mathf.RoundToInt(end.y);

        int dx = Mathf.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
        int dy = Mathf.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
        int err = (dx > dy ? dx : -dy) / 2, e2;

        while (true)
        {
            tex.SetPixel(x0, y0, color);
            if (x0 == x1 && y0 == y1) break;
            e2 = err;
            if (e2 > -dx) { err -= dy; x0 += sx; }
            if (e2 < dy) { err += dx; y0 += sy; }
        }
    }
}


