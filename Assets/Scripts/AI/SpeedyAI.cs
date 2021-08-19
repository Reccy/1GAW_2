using UnityEngine;

public class SpeedyAI : EnemyAI
{
    private Path m_path;
    private Tile m_target;

    private Color DebugColor { get { return new Color(255.0f / 255.0f, 184.0f / 255.0f, 255.0f / 255.0f); } }

    public Tile GetTarget()
    {
        return m_target;
    }

    private Vector2Int EnemyToPlayer(Enemy enemy, Level level)
    {
        PachMan pachMan = level.PachMan;

        Tile current = enemy.GetTile();
        Tile pachTile = pachMan.GetTile();

        Vector2Int targetPos = pachTile.Position + pachMan.Direction * 2 * new Vector2Int(1, -1);
        targetPos.Clamp(Vector2Int.zero, new Vector2Int(level.Width - 1, level.Height - 1));

        Tile target = level.GetTileAt(targetPos);

        m_target = level.GetClosestPassableTile(target);

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
