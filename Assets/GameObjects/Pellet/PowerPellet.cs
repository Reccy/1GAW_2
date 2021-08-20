using UnityEngine;

[SelectionBase]
public class PowerPellet : MonoBehaviour
{
    private GameManager m_gameManager;
    private SpriteRenderer m_spriteRenderer;
    private bool m_collected = false;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_collected)
            return;

        if (!collision.gameObject.CompareTag("PachMan"))
            return;

        m_gameManager.AddScore(50);
        m_spriteRenderer.enabled = false;
        m_collected = true;
    }
}
