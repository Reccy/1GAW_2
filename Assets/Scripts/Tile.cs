using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    public enum PassMode
    {
        IMPASSABLE,
        ENEMY_ONLY,
        PASSABLE
    }

    private Level m_level;
    private Vector2Int m_pos;

    [SerializeField]
    private PassMode m_passMode;

    public PassMode Pass { get { return m_passMode; } }

    public void SubscribeToLevel(Level level)
    {
        m_level = level;
        m_pos = m_level.GetPosition(this);
    }

    public Tile NorthNeighbour()
    {
        return m_level.GetTileAt(m_pos.x, m_pos.y + 1);
    }

    public Tile SouthNeighbour()
    {
        return m_level.GetTileAt(m_pos.x, m_pos.y - 1);
    }

    public Tile EastNeighbour()
    {
        return m_level.GetTileAt(m_pos.x + 1, m_pos.y);
    }

    public Tile WestNeighbour()
    {
        return m_level.GetTileAt(m_pos.x - 1, m_pos.y);
    }
}
