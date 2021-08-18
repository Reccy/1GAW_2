using UnityEngine;

[SelectionBase]
public class PachMan : MonoBehaviour
{
    private Actor m_actor;

    public Actor Actor {
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

    public Vector2Int Direction { get { return m_actor.Direction; } }

    public void SubscribeToLevel(Level level, Tile startTile, Vector2Int pos, Vector2Int dir)
    {
        Actor.SubscribeToLevel(level, startTile, pos, dir);
    }

    private void Awake()
    {
        m_actor = GetComponent<Actor>();
    }

    private void Update()
    {
        ReadInput();
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
}
