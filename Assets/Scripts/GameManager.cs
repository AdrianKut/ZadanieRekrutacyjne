using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private ScoreManager m_ScoreManager;
    [SerializeField] private PlayerInGameInputManager m_InputManager;
    public ScoreManager GetScoreManager()
    {
        return m_ScoreManager;
    }

    protected override void Awake()
    {
        base.Awake();

        m_ScoreManager.Initialize();
        m_InputManager.Initialize();
    }

    private void OnDestroy()
    {
        m_ScoreManager.UnInitialize();
        m_InputManager.UnInitialize();
    }
}
