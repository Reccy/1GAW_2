using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private List<Tile> m_tiles;

    public Vector2Int MovementVector {
        get {
            Vector2Int from = m_tiles[0].RealPosition;
            Vector2Int to = m_tiles[1].RealPosition;

            DebugDraw.DrawArrow(from, to, Color.green);

            return to - from;
        }
    }

    public Path()
    {
        m_tiles = new List<Tile>();
    }

    public Tile NextFrom(Tile from)
    {
        return m_tiles[0];
    }

    public void Add(Tile tile)
    {
        m_tiles.Add(tile);
    }

    public void Reverse()
    {
        m_tiles.Reverse();
    }

    public void DrawDebug(Color color)
    {
        for (int i = 1; i < m_tiles.Count; ++i)
        {
            DebugDraw.DrawArrow(m_tiles[i - 1].transform.position, m_tiles[i].transform.position, color);
        }
    }
}
