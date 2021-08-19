using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum AI
    {
        AGGRESSIVE,
        SPEEDY,
        BASHFUL
    }

    [SerializeField]
    private AI m_aiMode;

    private EnemyAI m_ai;

    public bool IsAggressive { get { return m_aiMode == AI.AGGRESSIVE; } }
    public bool IsSpeedy { get { return m_aiMode == AI.SPEEDY; } }
    public bool IsBashful { get { return m_aiMode == AI.BASHFUL; } }

    private Actor m_actor;
    public Actor Actor
    {
        get
        {
            if (m_actor == null)
                m_actor = GetComponent<Actor>();

            return m_actor;
        }
    }

    public Tile GetTile()
    {
        return Actor.CurrentTile;
    }

    public Tile GetForwardTile()
    {
        Tile c = Actor.CurrentTile;
        Vector2Int cNext = c.Position + (Actor.Direction * new Vector2Int(1, -1));

        return m_level.GetTileAt(cNext);
    }

    public Tile GetBackwardTile()
    {
        Tile c = Actor.CurrentTile;
        Vector2Int cBack = c.Position + (-Actor.Direction * new Vector2Int(1, -1));

        return m_level.GetTileAt(cBack);
    }

    private Level m_level;

    public void SubscribeToLevel(Level level, Tile startTile, Vector2Int pos, Vector2Int dir)
    {
        m_level = level;
        Actor.SubscribeToLevel(level, startTile, pos, dir);
    }

    private void Awake()
    {
        switch (m_aiMode)
        {
            case AI.AGGRESSIVE:
                m_ai = new AggressiveAI();
                break;
            case AI.SPEEDY:
                m_ai = new SpeedyAI();
                break;
            case AI.BASHFUL:
                m_ai = new BashfulAI();
                break;
        }
    }

    private void Update()
    {
        Actor.InputDir = m_ai.EvaluateInputDir(this, m_level);
    }
}
