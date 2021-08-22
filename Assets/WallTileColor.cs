using UnityEngine;

public class WallTileColor : MonoBehaviour
{
    private GameManager m_gameManager;
    private Animator m_animator;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_gameManager.IsGameWon)
        {
            m_animator.SetBool("Won", true);
        }
        else
        {
            m_animator.SetBool("Won", false);
        }
    }
}
