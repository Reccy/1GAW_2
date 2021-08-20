using UnityEngine;

public class Actor : MonoBehaviour
{
    private Level m_level;
    private Tile m_currentTile;

    public Tile CurrentTile { get { return m_currentTile; } }

    private Tile m_lastTile;
    private Vector2Int m_position;

    public Vector2Int InputDir;

    private Vector2Int m_currentDirection;

    [SerializeField]
    private int m_framesPerTile = 8;
    private int m_currentFrame = 0;

    [SerializeField]
    private bool m_isEnemy = false;

    public Vector2Int Position { get { return m_position; } }
    public Vector2Int Direction { get { return m_currentDirection; } }

    private Tile NorthTile { get { return m_currentTile.NorthNeighbour(); } }
    private Tile SouthTile { get { return m_currentTile.SouthNeighbour(); } }
    private Tile EastTile { get { return m_currentTile.EastNeighbour(); } }
    private Tile WestTile { get { return m_currentTile.WestNeighbour(); } }

    private bool IsMovingNorth { get { return m_currentDirection.y == 1; } }
    private bool IsMovingSouth { get { return m_currentDirection.y == -1; } }
    private bool IsMovingEast { get { return m_currentDirection.x == 1; } }
    private bool IsMovingWest { get { return m_currentDirection.x == -1; } }

    private bool IsInputtingNorth { get { return InputDir.y == 1; } }
    private bool IsInputtingSouth { get { return InputDir.y == -1; } }
    private bool IsInputtingEast { get { return InputDir.x == 1; } }
    private bool IsInputtingWest { get { return InputDir.x == -1; } }

    public bool IsMoving { get { return m_currentTile.transform.position != transform.position; } }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            // Warp code
            if (Direction == Vector2Int.right && CurrentTile.RealPosition.x < m_lastTile.RealPosition.x)
            {
                transform.position = m_currentTile.transform.position;
                m_currentFrame = m_framesPerTile;
            }
            else if (Direction == Vector2Int.left && CurrentTile.RealPosition.x > m_lastTile.RealPosition.x)
            {
                transform.position = m_currentTile.transform.position;
                m_currentFrame = m_framesPerTile;
            }
            else
            {
                // Normal move
                transform.position = Vector2.Lerp(m_lastTile.transform.position, m_currentTile.transform.position, m_currentFrame / (float)m_framesPerTile);
            }
        }

        if (m_currentFrame >= m_framesPerTile)
        {
            m_currentFrame = 0;
            Move();
        }

        m_currentFrame++;
    }

    private bool IsPassable(Tile tile)
    {
        if (tile.IsPassable)
            return true;

        if (!m_isEnemy)
            return false;

        return tile.IsEnemyOnly;
    }

    private void Move()
    {
        // Read Input
        if (IsPassable(NorthTile) && IsInputtingNorth)
        {
            m_currentDirection = Vector2Int.up;
        }

        if (IsPassable(SouthTile) && IsInputtingSouth)
        {
            m_currentDirection = Vector2Int.down;
        }

        if (IsPassable(EastTile) && IsInputtingEast)
        {
            m_currentDirection = Vector2Int.right;
        }

        if (IsPassable(WestTile) && IsInputtingWest)
        {
            m_currentDirection = Vector2Int.left;
        }

        // Apply Movement
        if (IsPassable(NorthTile) && IsMovingNorth)
        {
            MoveToTile(NorthTile);
            m_currentDirection = Vector2Int.up;
            return;
        }

        if (IsPassable(SouthTile) && IsMovingSouth)
        {
            MoveToTile(SouthTile);
            m_currentDirection = Vector2Int.down;
            return;
        }

        if (IsPassable(EastTile) && IsMovingEast)
        {
            MoveToTile(EastTile);
            m_currentDirection = Vector2Int.right;
            return;
        }

        if (IsPassable(WestTile) && IsMovingWest)
        {
            MoveToTile(WestTile);
            m_currentDirection = Vector2Int.left;
            return;
        }
    }

    private void MoveToTile(Tile tile)
    {
        m_lastTile = m_currentTile;
        m_currentTile = tile;
    }

    public void SubscribeToLevel(Level level, Tile startTile, Vector2Int pos, Vector2Int dir)
    {
        m_level = level;
        m_currentTile = startTile;
        m_lastTile = startTile;
        m_position = pos;
        m_currentDirection = dir;
        transform.position = m_currentTile.transform.position;
    }
}
