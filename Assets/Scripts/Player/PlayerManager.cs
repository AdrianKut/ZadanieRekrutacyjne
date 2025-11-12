using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerMovement m_PlayerMovement;
    public const string PlayerTag = "Player";

    protected override void Awake()
    {
        base.Awake();
        if( m_PlayerMovement == null )
        {
            Debug.LogError( "Missing PlayerMovement" );
        }
    }

    private void Start()
    {
        m_PlayerMovement.Initialize();
    }

    private void OnDestroy()
    {
        m_PlayerMovement.UnInitialize();
    }

    public Transform GetPlayerTransform()
    {
        return m_PlayerMovement.transform;
    }
}
