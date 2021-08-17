using UnityEngine;

public class Level : MonoBehaviour
{
    private int m_width = 28;
    private int m_height = 31;

    public int Height { get { return m_height; } }
    public int Width { get { return m_width; } }
    public int Size { get { return m_width * m_height; } }

    private Tile[] m_tiles;

    private void Awake()
    {
        LoadFromHierarchy();
    }

    private void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = transform.InverseTransformPoint(pos);

        Vector2Int p = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));

        Tile t = GetTileAt(p.x, p.y);

        Debug.Log($"{GetPosition(t)} - N:({GetPosition(t.NorthNeighbour())}, S:({GetPosition(t.SouthNeighbour())}), E:({GetPosition(t.EastNeighbour())}), W:({GetPosition(t.WestNeighbour())})");

        DebugDraw.DrawCross(pos, Color.red);
        DebugDraw.DrawCross(p, Color.blue);

        if (t == null)
            return;

        if (t.NorthNeighbour().Pass == Tile.PassMode.PASSABLE)
        {
            DebugDraw.DrawArrow(t.transform.position, t.NorthNeighbour().transform.position, Color.green);
        }
        else
        {
            DebugDraw.DrawArrow(t.transform.position, t.NorthNeighbour().transform.position, Color.red);
        }

        if (t.SouthNeighbour().Pass == Tile.PassMode.PASSABLE)
        {
            DebugDraw.DrawArrow(t.transform.position, t.SouthNeighbour().transform.position, Color.green);
        }
        else
        {
            DebugDraw.DrawArrow(t.transform.position, t.SouthNeighbour().transform.position, Color.red);
        }

        if (t.EastNeighbour().Pass == Tile.PassMode.PASSABLE)
        {
            DebugDraw.DrawArrow(t.transform.position, t.EastNeighbour().transform.position, Color.green);
        }
        else
        {
            DebugDraw.DrawArrow(t.transform.position, t.EastNeighbour().transform.position, Color.red);
        }

        if (t.WestNeighbour().Pass == Tile.PassMode.PASSABLE)
        {
            DebugDraw.DrawArrow(t.transform.position, t.WestNeighbour().transform.position, Color.green);
        }
        else
        {
            DebugDraw.DrawArrow(t.transform.position, t.WestNeighbour().transform.position, Color.red);
        }
    }

    public void LoadFromHierarchy()
    {
        m_tiles = GetComponentsInChildren<Tile>();

        foreach (Tile tile in m_tiles)
        {
            tile.SubscribeToLevel(this);
        }

        Debug.Log($"Loaded {m_tiles.Length} tiles");
    }

#if UNITY_EDITOR
    public bool Validate()
    {
        LoadFromHierarchy();

        bool valid = true;

        foreach (Tile tile in m_tiles)
        {
            Vector2Int position = GetPosition(tile);

            if (tile.transform.position.x != position.x || tile.transform.position.y != position.y)
            {
                Debug.LogWarning($"Tile {tile.name} has real position {tile.transform.position}, but index of {position}", tile);
                valid = false;
            }
        }

        if (!valid)
        {
            Debug.LogWarning("Validation complete with errors");
        }
        else
        {
            Debug.Log("Validation complete with no errors");
        }

        return valid;
    }
#endif

    public Vector2Int GetPosition(Tile tile)
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

    public Tile GetTileAt(int x, int y)
    {
        x = Mathf.Abs(x % m_width);
        y = Mathf.Abs(y % m_height);
        int i = x + y * m_width;

        Tile t = m_tiles[i];

        return t;
    }
}
