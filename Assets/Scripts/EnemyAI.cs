using UnityEngine;

public interface EnemyAI
{
    Vector2Int EvaluateInputDir(Enemy enemy, Level level);
    public Tile GetTarget();
}
