using UnityEngine;

public class Level : MonoBehaviour
{
    private int m_width = 28;
    private int m_height = 31;

    public int Height { get { return m_height; } }
    public int Width { get { return m_width; } }
    public int Size { get { return m_width * m_height; } }

    private Tile[] m_tiles;

    public void LoadFromHierarchy()
    {
        m_tiles = GetComponentsInChildren<Tile>();
        Debug.Log($"Loaded {m_tiles.Length} tiles");
    }

    public void Validate()
    {
        LoadFromHierarchy();

        foreach (Tile tile in m_tiles)
        {
            Vector2Int position = GetPosition(tile);

            if (tile.transform.position.x != position.x && tile.transform.position.y != position.y)
                Debug.LogWarning($"Tile {tile.name} has real position {tile.transform.position}, but index of {position}", tile);
        }

        Debug.Log("Validation complete");

        m_tiles = null;
    }

    private Vector2Int GetPosition(Tile tile)
    {
        for (int x = 0; x < m_width; ++x)
        {
            for (int y = 0; y < m_height; ++y)
            {
                int i = x + y * m_width;

                Tile foundTile = m_tiles[i];

                if (tile == foundTile)
                    return new Vector2Int(x, y);
            }
        }

        return new Vector2Int(-1, -1);
    }
}
