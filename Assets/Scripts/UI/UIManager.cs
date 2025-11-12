using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_ExitButton;

    [Header( "SettingsPanel" )]
    [SerializeField] private GameObject m_SettingsPanel = null;
    [SerializeField] private Button m_SettingsButtonBack = null;

    private PlayerControls m_PlayerControls;

    private void Awake()
    {
        BindAction();
        SetupInputSystem();
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject( m_PlayButton.gameObject );
    }

    private void SetupInputSystem()
    {
        m_PlayerControls = new PlayerControls();

        m_PlayerControls.UI.Navigate.performed += ctx => OnNavigate( ctx.ReadValue<Vector2>() );
        m_PlayerControls.UI.Submit.performed += ctx => OnSubmit();

        m_PlayerControls.UI.Enable();
        EventSystem.current.SetSelectedGameObject( m_PlayButton.gameObject );
    }

    private void OnNavigate( Vector2 direction )
    {
        var current = EventSystem.current.currentSelectedGameObject;
        if( current == null ) return;

        var selectable = current.GetComponent<Selectable>();
        if( selectable == null ) return;

        Selectable next = null;

        if( Mathf.Abs( direction.x ) > Mathf.Abs( direction.y ) )
        {
            if( direction.x > 0 ) next = selectable.FindSelectableOnRight();
            else if( direction.x < 0 ) next = selectable.FindSelectableOnLeft();
        }
        else
        {
            if( direction.y > 0 ) next = selectable.FindSelectableOnUp();
            else if( direction.y < 0 ) next = selectable.FindSelectableOnDown();
        }

        if( next != null )
            next.Select();
    }

    private void OnSubmit()
    {
        var current = EventSystem.current.currentSelectedGameObject;
        if( current == null ) return;

        var button = current.GetComponent<Button>();
        if( button != null )
            button.onClick.Invoke();
    }

    private void BindAction()
    {
        m_PlayButton.onClick.AddListener( HandlePlayButtonClicked );
        m_SettingsButton.onClick.AddListener( HandleSettingsButtonClicked );
        m_ExitButton.onClick.AddListener( HandleExitButtonClicked );
        m_SettingsButtonBack.onClick.AddListener( HandleSettingsButtonBackClicked );
    }

    private void UnBindAction()
    {
        m_PlayButton.onClick.RemoveListener( HandlePlayButtonClicked );
        m_SettingsButton.onClick.RemoveListener( HandleSettingsButtonClicked );
        m_ExitButton.onClick.RemoveListener( HandleExitButtonClicked );
        m_SettingsButtonBack.onClick.RemoveListener( HandleSettingsButtonBackClicked );
    }

    private void OnDisable()
    {
        UnBindAction();

        if( m_PlayerControls != null )
        {
            m_PlayerControls.UI.Navigate.performed -= ctx => OnNavigate( ctx.ReadValue<Vector2>() );
            m_PlayerControls.UI.Submit.performed -= ctx => OnSubmit();
            m_PlayerControls.UI.Disable();
        }
    }

    private void OnDestroy()
    {
        UnBindAction();
    }

    private void HandlePlayButtonClicked()
    {
        SceneManager.LoadScene( SceneNameProvider.GAME_SCENE_KEY );
    }

    private void HandleSettingsButtonClicked()
    {
        m_SettingsPanel.SetActive( true );
        EventSystem.current.SetSelectedGameObject( m_SettingsButtonBack.gameObject );
    }

    private void HandleSettingsButtonBackClicked()
    {
        m_SettingsPanel.SetActive( false );
        EventSystem.current.SetSelectedGameObject( m_SettingsButton.gameObject );
    }

    private void HandleExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
