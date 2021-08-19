using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int m_width = 28;
    private int m_height = 31;

    [Header("Player Setup")]
    [SerializeField]
    private Vector2Int m_playerStartPosition;

    [SerializeField]
    private Vector2Int m_playerStartDirection;

    [Header("Enemy Setup")]
    [SerializeField]
    private Vector2Int m_redEnemyStartPosition;

    [SerializeField]
    private Vector2Int m_redEnemyStartDirection;

    [SerializeField]
    private Vector2Int m_pinkEnemyStartPosition;

    [SerializeField]
    private Vector2Int m_pinkEnemyStartDirection;

    [SerializeField]
    private Vector2Int m_blueEnemyStartPosition;

    [SerializeField]
    private Vector2Int m_blueEnemyStartDirection;

    private PachMan m_pachMan;
    public PachMan PachMan { get { return m_pachMan; } }

    private Enemy m_redEnemy;
    private Enemy m_pinkEnemy;
    private Enemy m_blueEnemy;

    public Enemy RedEnemy { get { return m_redEnemy; } }
    public Enemy PinkEnemy { get { return m_pinkEnemy; } }
    public Enemy BlueEnemy { get { return m_blueEnemy; } }

    public int Height { get { return m_height; } }
    public int Width { get { return m_width; } }
    public int Size { get { return m_width * m_height; } }

    private Tile[] m_tiles;

    private void Awake()
    {
        LoadFromHierarchy();
        InitPlayer();
        InitEnemies();
    }

    private void LoadFromHierarchy()
    {
        m_tiles = GetComponentsInChildren<Tile>();

        foreach (Tile tile in m_tiles)
        {
            tile.SubscribeToLevel(this);
        }

        Debug.Log($"Loaded {m_tiles.Length} tiles");
    }

    private void InitPlayer()
    {
        m_pachMan = FindObjectOfType<PachMan>();
        m_pachMan.SubscribeToLevel(this, GetTileAt(m_playerStartPosition), m_playerStartPosition, m_playerStartDirection);
    }

    private void InitEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemies)
        {
            if (e.IsAggressive)
            {
                m_redEnemy = e;
                e.SubscribeToLevel(this, GetTileAt(m_redEnemyStartPosition), m_redEnemyStartPosition, m_redEnemyStartDirection);
            }

            if (e.IsSpeedy)
            {
                m_pinkEnemy = e;
                e.SubscribeToLevel(this, GetTileAt(m_pinkEnemyStartPosition), m_pinkEnemyStartPosition, m_pinkEnemyStartDirection);
            }

            if (e.IsBashful)
            {
                m_blueEnemy = e;
                e.SubscribeToLevel(this, GetTileAt(m_blueEnemyStartPosition), m_blueEnemyStartPosition, m_blueEnemyStartDirection);
            }
        }
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

    public Tile GetTileAt(Vector2Int pos)
    {
        return GetTileAt(pos.x, pos.y);
    }

    public Path Pathfind(Tile from, Tile to, Tile backward)
    {
        // Prepare A*
        Tile startTile = from;
        Tile endTile = to;

        Tile current;
        List<Tile> frontier = new List<Tile>();
        Dictionary<Tile, float> cellPriority = new Dictionary<Tile, float>();
        Dictionary<Tile, float> currentCost = new Dictionary<Tile, float>();
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();

        // Start the algorithm
        frontier.Add(startTile);
        cellPriority.Add(startTile, 0);
        currentCost.Add(startTile, 0);
        cameFrom.Add(startTile, null);

        while (frontier.Count > 0)
        {
            current = cellPriority.GetSmallest();
            cellPriority.Remove(current);

            if (current == endTile)
                break;

            foreach (Tile next in current.NeighboursExclude(backward))
            {
                if (next.IsImpassable)
                    continue;

                float newCost = currentCost[current] + Vector2Int.Distance(current.Position, next.Position);

                if (!currentCost.ContainsKey(next) || newCost < currentCost[next])
                {
                    currentCost[next] = newCost;

                    float priority = newCost + Vector2Int.Distance(next.Position, endTile.Position);

                    if (!cellPriority.ContainsKey(next))
                        cellPriority.Add(next, priority);
                    else
                        cellPriority[next] = priority;

                    frontier.Add(next);

                    if (!cameFrom.ContainsKey(next))
                        cameFrom.Add(next, current);
                    else
                        cameFrom[next] = current;
                }
            }
        }

        // Start backtracking through cameFrom to find the cell
        Path path = new Path();

        current = endTile;
        while (cameFrom[current] != null)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        // Add original start and end positions to path
        path.Add(startTile);
        path.Reverse();
        path.Add(endTile);

        return path;
    }
}
