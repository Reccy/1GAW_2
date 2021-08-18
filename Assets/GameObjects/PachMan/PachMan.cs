using UnityEngine;

public class PachMan : MonoBehaviour
{
    private Level m_level;
    private Tile m_currentTile;
    private Tile m_lastTile;
    private Vector2Int m_position;

    private Vector2Int m_inputDir;
    private Vector2Int m_currentDirection;

    [SerializeField]
    private int m_framesPerTile = 8;
    private int m_currentFrame = 0;

    public Vector2Int Position { get { return m_position; } }

    private Tile NorthTile { get { return m_currentTile.NorthNeighbour(); } }
    private Tile SouthTile { get { return m_currentTile.SouthNeighbour(); } }
    private Tile EastTile { get { return m_currentTile.EastNeighbour(); } }
    private Tile WestTile { get { return m_currentTile.WestNeighbour(); } }

    private bool IsMovingNorth { get { return m_currentDirection.y == 1; } }
    private bool IsMovingSouth { get { return m_currentDirection.y == -1; } }
    private bool IsMovingEast { get { return m_currentDirection.x == 1; } }
    private bool IsMovingWest { get { return m_currentDirection.x == -1; } }

    private bool IsInputtingNorth { get { return m_inputDir.y == 1; } }
    private bool IsInputtingSouth { get { return m_inputDir.y == -1; } }
    private bool IsInputtingEast { get { return m_inputDir.x == 1; } }
    private bool IsInputtingWest { get { return m_inputDir.x == -1; } }

    private bool IsMoving { get { return m_currentTile.transform.position != transform.position; } }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            transform.position = Vector2.Lerp(m_lastTile.transform.position, m_currentTile.transform.position, m_currentFrame / (float)m_framesPerTile);
        }

        DebugDraw.DrawArrow(m_lastTile.transform.position, m_currentTile.transform.position, Color.red);

        if (m_currentFrame >= m_framesPerTile)
        {
            m_currentFrame = 0;
            Move();
        }

        m_currentFrame++;
    }

    private void ReadInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            m_inputDir = Vector2Int.left;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            m_inputDir = Vector2Int.up;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            m_inputDir = Vector2Int.right;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            m_inputDir = Vector2Int.down;
        }
    }

    private void Move()
    {
        // Read Input
        if (NorthTile.IsPassable && IsInputtingNorth)
        {
            m_currentDirection = Vector2Int.up;
        }

        if (SouthTile.IsPassable && IsInputtingSouth)
        {
            m_currentDirection = Vector2Int.down;
        }

        if (EastTile.IsPassable && IsInputtingEast)
        {
            m_currentDirection = Vector2Int.right;
        }

        if (WestTile.IsPassable && IsInputtingWest)
        {
            m_currentDirection = Vector2Int.left;
        }

        // Apply Movement
        if (NorthTile.IsPassable && IsMovingNorth)
        {
            MoveToTile(NorthTile);
            m_currentDirection = Vector2Int.up;
            return;
        }

        if (SouthTile.IsPassable && IsMovingSouth)
        {
            MoveToTile(SouthTile);
            m_currentDirection = Vector2Int.down;
            return;
        }

        if (EastTile.IsPassable && IsMovingEast)
        {
            MoveToTile(EastTile);
            m_currentDirection = Vector2Int.right;
            return;
        }

        if (WestTile.IsPassable && IsMovingWest)
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
