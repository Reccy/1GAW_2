using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum AI
    {
        AGGRESSIVE,
        SPEEDY
    }

    [SerializeField]
    private AI m_aiMode;

    private EnemyAI m_ai;

    public bool IsAggressive { get { return m_aiMode == AI.AGGRESSIVE; } }
    public bool IsSpeedy { get { return m_aiMode == AI.SPEEDY; } }

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
        }
    }

    private void Update()
    {
        Actor.InputDir = m_ai.EvaluateInputDir(this, m_level);
    }
}
