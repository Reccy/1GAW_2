using UnityEngine;

public class PokeyAI : EnemyAI
{
    private Path m_path;
    private Tile m_target;

    private Vector2Int ScatterTileCoordinates { get { return new Vector2Int(0, 30); } }

    private Color DebugColor { get { return new Color(255.0f/255.0f, 184.0f/255.0f, 81.0f/255.0f); } }

    public Tile GetTarget()
    {
        return m_target;
    }

    private Vector2Int EnemyToPlayer(Enemy enemy, Level level)
    {
        Tile current = enemy.GetTile();
        PachMan pachMan = level.PachMan;

        float dist = Vector2Int.Distance(enemy.GetTile().Position, pachMan.GetTile().Position);

        if (dist > 8)
        {
            m_target = pachMan.GetTile();
        }
        else
        {
            m_target = level.GetTileAt(ScatterTileCoordinates);
            m_target = level.GetClosestPassableTile(m_target);

            if (current == m_target)
            {
                Tile tempTarget = m_target.PassableNeighbour(enemy.GetBackwardTile());
                return tempTarget.RealPosition - current.RealPosition;
            }
        }

        DebugDraw.DrawCross(m_target.transform.position, DebugColor);
        
        m_path = level.Pathfind(current, m_target, enemy.GetBackwardTile());
        
        m_path.DrawDebug(DebugColor);

        return m_path.MovementVector;
    }

    public Vector2Int EvaluateInputDir(Enemy enemy, Level level)
    {
        var r = EnemyToPlayer(enemy, level);
        return r;
    }
}
