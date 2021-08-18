using UnityEngine;

public class Level : MonoBehaviour
{
    private int m_width = 28;
    private int m_height = 31;

    public int Height { get { return m_height; } }
    public int Width { get { return m_width; } }
    public int Size { get { return m_width * m_height; } }

    private Tile[] m_tiles;

    private Vector2Int m_position = new Vector2Int(10, 9);
    private Tile m_currentTile;

    private void Awake()
    {
        LoadFromHierarchy();
        m_currentTile = GetTileAt(m_position.x, m_position.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Tile target = m_currentTile.WestNeighbour();

            if (target.Pass == Tile.PassMode.PASSABLE)
                m_currentTile = target;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Tile target = m_currentTile.NorthNeighbour();

            if (target.Pass == Tile.PassMode.PASSABLE)
                m_currentTile = target;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Tile target = m_currentTile.EastNeighbour();

            if (target.Pass == Tile.PassMode.PASSABLE)
                m_currentTile = target;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Tile target = m_currentTile.SouthNeighbour();

            if (target.Pass == Tile.PassMode.PASSABLE)
                m_currentTile = target;
        }

        Vector2Int p = m_currentTile.Position;

        DebugDraw.DrawCross(m_currentTile.transform.position, Color.blue);
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

        Debug.LogError($"Tried to get position for invalid tile: {tile.name}");
        return new Vector2Int(-1, -1);
    }

    int Mod(int a, int n) => (a % n + n) % n;

    public Tile GetTileAt(int x, int y)
    {
        x = Mod(x, m_width);
        y = Mod(y, m_height);
        int i = x + y * m_width;

        Tile t = m_tiles[i];

        return t;
    }
}
