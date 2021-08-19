using UnityEngine;

public class BashfulAI : EnemyAI
{
    private Path m_path;

    private Vector2Int EnemyToPlayer(Enemy enemy, Level level)
    {
        PachMan pachMan = level.PachMan;
        Enemy redEnemy = level.RedEnemy;

        Tile current = enemy.GetTile();
        Tile forward = enemy.GetForwardTile();
        Tile pachTile = pachMan.GetTile();
        Vector2Int pachTileForwardPos = pachTile.Position + pachMan.Direction * 2;

        Tile pachTileForward = level.GetTileAt(pachTileForwardPos);

        DebugDraw.DrawArrow(current.transform.position, forward.transform.position, Color.green);
        DebugDraw.DrawArrow(current.transform.position, enemy.GetBackwardTile().transform.position, Color.blue);

        m_path = level.Pathfind(current, pachTile, forward);

        m_path.DrawDebug(Color.blue);

        return m_path.MovementVector;
    }

    public Vector2Int EvaluateInputDir(Enemy enemy, Level level)
    {
        var r = EnemyToPlayer(enemy, level);
        return r;
    }
}
