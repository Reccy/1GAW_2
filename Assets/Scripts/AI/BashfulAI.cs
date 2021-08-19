using UnityEngine;

public class BashfulAI : EnemyAI
{
    private Path m_path;
    private Tile m_target;

    private Vector2Int ScatterTileCoordinates { get { return new Vector2Int(28, 30); } }

    private Color DebugColor { get { return new Color(1.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f); } }

    public Tile GetTarget()
    {
        return m_target;
    }

    private Vector2Int EnemyToPlayer(Enemy enemy, Level level)
    {
        PachMan pachMan = level.PachMan;
        Enemy redEnemy = level.RedEnemy;

        Tile current = enemy.GetTile();

        Tile pachManForwardTile = level.GetTileAt(pachMan.GetTile().Position + pachMan.Direction * 2 * new Vector2Int(1, -1));

        Vector2Int redToPach = pachManForwardTile.Position - redEnemy.GetTile().Position;
        redToPach *= 2;

        Vector2Int targetPos = redEnemy.GetTile().Position + redToPach;
        targetPos.Clamp(Vector2Int.zero, new Vector2Int(level.Width - 1, level.Height - 1));

        Tile target = level.GetTileAt(targetPos);

        m_target = level.GetClosestPassableTile(target);

        //DebugDraw.DrawCross(pachManForwardTile.RealPosition, DebugColor);
        DebugDraw.DrawCross(m_target.RealPosition, DebugColor);
        DebugDraw.DrawArrow(redEnemy.transform.position, pachManForwardTile.transform.position, Color.red);
        DebugDraw.DrawArrow(pachManForwardTile.transform.position, target.transform.position, Color.green);

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
