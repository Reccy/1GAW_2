using UnityEngine;

[SelectionBase]
public class PowerPellet : MonoBehaviour
{
    private GameManager m_gameManager;
    private SpriteRenderer m_spriteRenderer;
    private bool m_collected = false;

    private PachMan m_pachMan;

    [SerializeField]
    private Tile nextTile;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_pachMan = FindObjectOfType<PachMan>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_collected)
            return;

        if (!collision.gameObject.CompareTag("PachMan"))
            return;

        if (m_pachMan.IsReadyToUsePowerPellet)
        {
            m_pachMan.MoveTo(nextTile);
        }
    }
}
