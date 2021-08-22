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
    private int m_hiScore = 0;

    private int m_currentStateFrame = 0;
    private int m_stateFrames = 60 * 3;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        m_state = GameState.GAME_START;
    }

    private void FixedUpdate()
    {
        if (IsGameStarting)
        {
            if (m_currentStateFrame < m_stateFrames)
            {
                m_currentStateFrame++;
            }
            else
            {
                m_state = GameState.GAME_RUN;
                m_currentStateFrame = 0;
            }
        }

        if (IsGameLost)
        {
            if (m_currentStateFrame < m_stateFrames)
            {
                m_currentStateFrame++;
            }
            else
            {
                m_state = GameState.GAME_START;
                m_currentStateFrame = 0;
            }
        }
    }

    public int GetScore()
    {
        return m_score;
    }

    public int GetHiScore()
    {
        return m_hiScore;
    }

    public void AddScore(int amount)
    {
        m_score += amount;

        m_hiScore = Mathf.Max(m_hiScore, m_score);
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
