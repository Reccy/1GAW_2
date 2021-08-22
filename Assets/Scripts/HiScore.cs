using UnityEngine;
using TMPro;

public class HiScore : MonoBehaviour
{
    private GameManager m_gameManager;
    private TMP_Text m_ui;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_ui = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        int score = m_gameManager.GetHiScore();
        m_ui.text = $"HI {score}";
    }
}
