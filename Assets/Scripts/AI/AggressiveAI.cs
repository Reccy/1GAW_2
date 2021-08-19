using UnityEngine;

public class AggressiveAI : EnemyAI
{
    private Path m_path;

    private Vector2Int EnemyToPlayer(Enemy enemy, Level level)
    {
        PachMan pachMan = level.PachMan;

        Tile current = enemy.GetTile();
        Tile target = pachMan.GetTile();

        DebugDraw.DrawArrow(current.transform.position, enemy.GetBackwardTile().transform.position, Color.blue);

        m_path = level.Pathfind(current, target, enemy.GetBackwardTile());

        m_path.DrawDebug(Color.red);

        return m_path.MovementVector;
    }

    public Vector2Int EvaluateInputDir(Enemy enemy, Level level)
    {
        var r = EnemyToPlayer(enemy, level);
        return r;
    }
}
