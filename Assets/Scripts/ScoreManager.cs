using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreText = null;
    public Action OnPlayerEnemyTouched = null;
    private int m_Score = 0;
    private int m_MaxScore = 0;

    public void Initialize()
    {
        BindAction();
    }

    private void Start()
    {
        SetupScore();
    }

    private void SetupScore()
    {
        if( EnemyManager.Instance != null )
        {
            m_MaxScore = EnemyManager.Instance.GetEnemiesCount();
        }
        else
        {
            Debug.LogError( "Missing Enemy Manager" );
        }

        UpdateText();
    }

    public void UnInitialize()
    {
        UnBindAction();
    }

    private void BindAction()
    {
        OnPlayerEnemyTouched += HandlePlayerEnemyTouched;
    }

    private void UnBindAction()
    {
        OnPlayerEnemyTouched += HandlePlayerEnemyTouched;
    }

    private void HandlePlayerEnemyTouched()
    {
        m_Score++;
        UpdateText();
    }

    private void UpdateText()
    {
        m_ScoreText.text = $"{m_Score}/{m_MaxScore}";
    }
}
