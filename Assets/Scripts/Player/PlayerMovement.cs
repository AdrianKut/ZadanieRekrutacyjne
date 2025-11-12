using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header( "Move" )]
    [SerializeField] private Rigidbody2D m_Rigidbody = null;
    [SerializeField] private float m_MoveSpeed = 5f;
    Vector2 m_MoveDirection = Vector2.zero;

    private PlayerControls m_PlayerControls = null;
    private InputAction m_MoveAction;
    private bool m_IsMoving = false;

    public void Initialize()
    {
        m_IsMoving = true;
        m_PlayerControls = new PlayerControls();
        m_PlayerControls.Enable();

        m_MoveAction = m_PlayerControls.Player.Move;
        m_MoveAction.Enable();
    }

    public void UnInitialize()
    {
        m_IsMoving = false;
        m_PlayerControls.Disable();
        m_MoveAction.Disable();
    }

    private void Update()
    {
        if( m_IsMoving )
        {
            m_MoveDirection = m_MoveAction.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody.linearVelocity = new Vector2( m_MoveDirection.x * m_MoveSpeed, m_MoveDirection.y * m_MoveSpeed );
    }
}
