using TMPro;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_Num;

    [Space]
    [SerializeField] private float m_OffsetScreenMin = 0.05f;
    [SerializeField] private float m_OffsetScreenMax = 0.95f;
    [SerializeField] private Rigidbody2D m_Rigidbody2D = null;

    [Header( "Colorize" )]
    [SerializeField] private SpriteRenderer m_SpriteRenderer = null;
    [SerializeField] private Color32 m_OnTouchedColor = Color.red;

    [Space]
    [SerializeField] private float m_MoveSpeed = 0.5f;

    private int ID = 0;
    private bool m_CanMove = false;
    private ScoreManager m_ScoreManager = null;
    private Transform m_Player = null;

    private void Start()
    {
        m_Player = PlayerManager.Instance.GetPlayerTransform();
        m_ScoreManager = GameManager.Instance.GetScoreManager();
        if( m_ScoreManager == null )
        {
            Debug.LogError( "Missing Score Manager" );
        }
    }

    public void Initialize( int index )
    {
        ID = index;
        m_Num.text = $"{ID}";
        m_CanMove = true;
    }

    public void UnInitialize()
    {
        m_CanMove = false;
    }

    private void OnDestroy()
    {
        UnInitialize();
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision != null
            && collision.gameObject.tag == PlayerManager.PlayerTag )
        {
            m_CanMove = false;
            m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            m_SpriteRenderer.color = m_OnTouchedColor;

            m_ScoreManager.OnPlayerEnemyTouched?.Invoke();
        }
    }

    private void Update()
    {
        if( m_CanMove )
        {
            MoveAway();
        }
    }

    private void MoveAway()
    {
        Vector3 direction = (transform.position - m_Player.position).normalized;
        Vector3 newPosition = transform.position + direction * m_MoveSpeed * Time.deltaTime;

        Vector3 screenPos = Camera.main.WorldToViewportPoint( newPosition );

        if( screenPos.x < m_OffsetScreenMin || screenPos.x > m_OffsetScreenMax )
        {
            direction.x = 0;
        }
        if( screenPos.y < m_OffsetScreenMin || screenPos.y > m_OffsetScreenMax )
        {
            direction.y = 0;
        }

        transform.position += direction * m_MoveSpeed * Time.deltaTime;
    }
}
