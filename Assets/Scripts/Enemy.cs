using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    enum AI
    {
        AGGRESSIVE,
        SPEEDY,
        BASHFUL,
        POKEY
    }

    [SerializeField]
    private AI m_aiMode;

    private EnemyAI m_ai;

    public bool IsAggressive { get { return m_aiMode == AI.AGGRESSIVE; } }
    public bool IsSpeedy { get { return m_aiMode == AI.SPEEDY; } }
    public bool IsBashful { get { return m_aiMode == AI.BASHFUL; } }
    public bool IsPokey { get { return m_aiMode == AI.POKEY; } }

    private GameManager m_gameManager;
    private Animator m_animator;

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

    public Tile GetTarget()
    {
        return m_ai.GetTarget();
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
        m_animator = GetComponent<Animator>();
        m_gameManager = FindObjectOfType<GameManager>();

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
            case AI.POKEY:
                m_ai = new PokeyAI();
                break;
        }
    }

    private void FixedUpdate()
    {
        Actor.InputDir = m_ai.EvaluateInputDir(this, m_level);

        Tile nextTile = m_level.GetTileAt(Actor.CurrentTile.Position + Actor.InputDir);

        // If pathfinding makes us try to turn around, then ignore it and pick a different direction.
        // This will allow the character to loop around the block if the position stays stationary.
        if (nextTile == GetBackwardTile())
        {
            Actor.InputDir = Actor.CurrentTile.PassableNeighbour(GetBackwardTile()).RealPosition - Actor.CurrentTile.RealPosition;
        }
    }

    private void Update()
    {
        if (m_animator == null)
            return;

        m_animator.SetInteger("xDirection", Actor.Direction.x);
        m_animator.SetInteger("yDirection", Actor.Direction.y);

        if (m_gameManager.IsGameRunning)
            m_animator.SetFloat("Speed", 1.0f);
        else
            m_animator.SetFloat("Speed", 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PachMan"))
            return;

        FindObjectOfType<PachMan>().Kill();
    }

    public void Disappear()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void Appear()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    public void MoveTo(Tile dest, Vector2Int lookDir)
    {
        Actor.MoveTo(dest);
        Actor.Look(lookDir);
    }
}
