using System.Collections.Generic;
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

    public bool IsImpassable { get { return m_passMode == PassMode.IMPASSABLE; } }
    public bool IsEnemyOnly { get { return m_passMode == PassMode.ENEMY_ONLY; } }
    public bool IsPassable { get { return m_passMode == PassMode.PASSABLE; } }

    private Level m_level;
    private Vector2Int m_pos;

    [SerializeField]
    private PassMode m_passMode;

    public PassMode Pass { get { return m_passMode; } }

    public Vector2Int Position { get { return m_pos; } }

    public Vector2Int RealPosition { get { return new Vector2Int((int)transform.position.x, (int)transform.position.y); } }

    public void SubscribeToLevel(Level level)
    {
        m_level = level;
        m_pos = m_level.GetPosition(this);
    }

    public Tile NorthNeighbour()
    {
        return m_level.GetTileAt(m_pos.x, m_pos.y - 1);
    }

    public Tile SouthNeighbour()
    {
        return m_level.GetTileAt(m_pos.x, m_pos.y + 1);
    }

    public Tile EastNeighbour()
    {
        return m_level.GetTileAt(m_pos.x + 1, m_pos.y);
    }

    public Tile WestNeighbour()
    {
        return m_level.GetTileAt(m_pos.x - 1, m_pos.y);
    }

    public Tile[] Neighbours()
    {
        return new Tile[4] {
            NorthNeighbour(),
            SouthNeighbour(),
            EastNeighbour(),
            WestNeighbour()
        };
    }

    public Tile[] NeighboursExclude(Tile exclude)
    {
        List<Tile> tiles = new List<Tile>();

        foreach (Tile t in Neighbours())
        {
            if (exclude == t)
                continue;

            tiles.Add(t);
        }

        return tiles.ToArray();
    }
    
    public Tile PassableNeighbour(Tile exclude)
    {
        foreach (Tile t in NeighboursExclude(exclude))
        {
            if (t.IsPassable)
                return t;
        }

        Debug.LogWarning("Default PassableNeighbours() return. Logic error!", this);
        return Neighbours()[0];
    }

    public int GetAdjacentPassables()
    {
        int c = 0;

        foreach (Tile t in Neighbours())
        {
            if (t.IsPassable)
                c++;
        }

        return c;
    }
}
