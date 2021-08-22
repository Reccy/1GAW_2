using UnityEngine;
using TMPro;

public class ReadyUI : MonoBehaviour
{
    private TMP_Text m_text;
    private GameManager m_gameManager;

    private void Awake()
    {
        m_text = GetComponent<TMP_Text>();
        m_gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (m_gameManager.IsGameStarting)
        {
            m_text.enabled = true;
        }
        else
        {
            m_text.enabled = false;
        }
    }
}
