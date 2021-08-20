using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        GAME_START,
        GAME_RUN,
        GAME_END_WIN,
        GAME_END_LOSE
    }

    private GameState m_state;

    public bool IsGameStarting { get { return m_state == GameState.GAME_START; } }
    public bool IsGameRunning { get { return m_state == GameState.GAME_RUN; } }
    public bool IsGameWon { get { return m_state == GameState.GAME_END_WIN; } }
    public bool IsGameLost { get { return m_state == GameState.GAME_END_LOSE; } }

    private int m_score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        m_state = GameState.GAME_RUN;
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

    public void EndGameLose()
    {
        m_state = GameState.GAME_END_LOSE;
    }
}
