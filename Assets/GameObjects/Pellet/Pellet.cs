using UnityEngine;

[SelectionBase]
public class Pellet : MonoBehaviour
{
    private GameManager m_gameManager;
    private SpriteRenderer m_spriteRenderer;
    private bool m_collected = false;

    public bool IsCollected { get { return m_collected; } }

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

        m_gameManager.AddScore(10);
        m_spriteRenderer.enabled = false;
        m_collected = true;

        int collectedAmount = 0;
        var pellets = FindObjectsOfType<Pellet>();

        foreach (var pellet in pellets)
        {
            if (pellet.IsCollected)
                collectedAmount++;
        }

        if (pellets.Length == collectedAmount)
        {
            m_gameManager.WinGame();
        }
    }
}
