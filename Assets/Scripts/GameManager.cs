using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int m_score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public int GetScore()
    {
        return m_score;
    }

    public void AddScore(int amount)
    {
        m_score += amount;
    }

    public void ResetScore()
    {
        m_score = 0;
    }
}
