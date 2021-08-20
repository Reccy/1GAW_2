using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
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
        int score = m_gameManager.GetScore();
        m_ui.text = $"SCORE {score}";
    }
}
