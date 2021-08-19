using UnityEngine;

public class AggressiveAI : EnemyAI
{
    private Path m_path;
    private Tile m_target;

    private Vector2Int ScatterTileCoordinates { get { return new Vector2Int(27, 0); } }

    private Color DebugColor { get { return Color.red; } }

    public Tile GetTarget()
    {
        return m_target;
    }

    private Vector2Int EnemyToPlayer(Enemy enemy, Level level)
    {
        PachMan pachMan = level.PachMan;

        Tile current = enemy.GetTile();
        m_target = pachMan.GetTile();

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
