using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInGameInputManager : MonoBehaviour
{
    private PlayerControls m_PlayerControls = null;
    private InputAction m_EscapeAction;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        m_PlayerControls = new PlayerControls();
        m_PlayerControls.Enable();

        m_EscapeAction = m_PlayerControls.Player.Escape;
        m_EscapeAction.Enable();
        m_EscapeAction.performed += EscapeInputPerformed;
    }

    private void OnDestroy()
    {
        UnInitialize();
    }

    public void UnInitialize()
    {
        m_PlayerControls.Disable();
        m_EscapeAction.Disable();
    }

    private void EscapeInputPerformed( InputAction.CallbackContext obj )
    {
        SceneManager.LoadScene( SceneNameProvider.MENU_SCENE_KEY );
    }
}
