using UnityEngine;

[SelectionBase]
public class PachMan : MonoBehaviour
{
    private Animator m_animator;

    private Actor m_actor;

    public Actor Actor {
        get
        {
            if (m_actor == null)
                m_actor = GetComponent<Actor>();
            
            return m_actor;
        }
    }

    private GameManager m_gameManager;

    private SpriteRenderer m_spriteRenderer;

    private int m_powerPelletTimer = 0;

    [SerializeField]
    private int m_powerPelletTimerMax = 128;

    public bool IsReadyToUsePowerPellet { get { return m_powerPelletTimer == 0; } }

    public Tile GetTile()
    {
        return Actor.CurrentTile;
    }

    public Vector2Int Direction { get { return m_actor.Direction; } }

    public void SubscribeToLevel(Level level, Tile startTile, Vector2Int pos, Vector2Int dir)
    {
        Actor.SubscribeToLevel(level, startTile, pos, dir);
    }

    public void Kill()
    {
        m_gameManager.EndGameLose();
    }

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_actor = GetComponent<Actor>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (m_gameManager.IsGameRunning)
        {
            ReadInput();
        }
        else
        {
            m_actor.InputDir = Vector2Int.zero;
        }

        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (m_powerPelletTimer > 0)
            m_powerPelletTimer -= 1;
    }

    public void OnPachDisappear()
    {
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            e.Disappear();
        }
    }

    public void MoveTo(Tile dest)
    {
        m_powerPelletTimer = m_powerPelletTimerMax;
        Actor.MoveTo(dest);
    }

    private void ReadInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            m_actor.InputDir = Vector2Int.left;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            m_actor.InputDir = Vector2Int.up;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            m_actor.InputDir = Vector2Int.right;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            m_actor.InputDir = Vector2Int.down;
        }
    }

    private void UpdateAnimator()
    {
        if (IsReadyToUsePowerPellet)
            m_spriteRenderer.color = Color.white;
        else
            m_spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        if (m_gameManager.IsGameLost)
        {
            m_animator.SetBool("IsDead", true);
            return;
        }

        if (Actor.IsMoving)
        {
            m_animator.SetFloat("AnimSpeed", 1);
        }
        else
        {
            m_animator.SetFloat("AnimSpeed", 0);
        }

        m_animator.SetInteger("xDirection", Actor.Direction.x);
        m_animator.SetInteger("yDirection", Actor.Direction.y);
    }
}
