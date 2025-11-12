using TMPro;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_Num;

    [Space]
    [SerializeField] private float m_OffsetScreenMin = 0.05f;
    [SerializeField] private float m_OffsetScreenMax = 0.95f;
    [SerializeField] private Transform m_Player = null;
    [SerializeField] private Rigidbody2D m_Rigidbody2D = null;

    [Space]
    [SerializeField] private float m_MoveSpeed;
    private bool m_CanMove = false;
    private int ID = 0;

    private void Start()
    {
        m_Player = PlayerManager.Instance.GetPlayerTransform();
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
        if( collision != null && collision.gameObject.tag == PlayerManager.PlayerTag )
        {
            m_CanMove = false;
            m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
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
