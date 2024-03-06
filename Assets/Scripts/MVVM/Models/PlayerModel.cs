using Assets.Scripts.CSharpClasses.EditorExtensions;
using Assets.Scripts.CSharpClasses.Interfaces;
using Assets.Scripts.CSharpClasses.Reactive;
using UnityEngine;
using Zenject;

public class PlayerModel : MonoBehaviour, ICharacter
{
    #region SerializeField
    [SerializeField] private PlayerScriptableObject m_playerScriptableObject;
    #endregion

    private InputController m_inputController;

    private Rigidbody2D m_rigidbody;
    private Vector2 m_direction;
    private float m_currentDashDistance = 0f;
    private Vector2 m_dashDirection;

    public ReactiveProperty<float> CurrentHealth { get; set; } = new ReactiveProperty<float>();

    #region Inject
    [Inject]
    public void Init(InputController inputController)
    {
        m_inputController = inputController;
    }
    #endregion

    #region Unity Default Methods
    private void Awake()
    {
        m_inputController.OnMoveAction += MovePlayer;
        m_inputController.OnDashAction += DashPlayer;
    }

    private void OnDestroy()
    {
        m_inputController.OnMoveAction -= MovePlayer;
        m_inputController.OnDashAction -= DashPlayer;
    }

    void Start()
    {
        CurrentHealth.Value = m_playerScriptableObject.MaxHealth;
        m_rigidbody = GetComponent<Rigidbody2D>();
        CurrentHealth.OnValueChanged += HealthCheck;
    }

    void FixedUpdate()
    {
        m_rigidbody.MovePosition(GetMoveVector() + GetDashVector());
    }
    #endregion

    #region Input Controller Methods
    private void MovePlayer(Vector2 position)
    {
        m_direction = position;
    }

    private void DashPlayer()
    {
        m_currentDashDistance = m_playerScriptableObject.DashDistance;
    }
    #endregion

    #region Get Vector Methods
    private Vector2 GetMoveVector()
    {
        return m_rigidbody.position + m_playerScriptableObject.PlayerSpeed * Time.deltaTime * m_direction;
    }

    private Vector2 GetDashVector()
    {
        if (m_direction != Vector2.zero)
        {
            m_dashDirection = m_direction;
        }

        Vector2 dashVector = m_currentDashDistance * m_dashDirection;
        m_currentDashDistance = 0f;

        return dashVector;
    }
    #endregion

    #region Player Methods
    private void HealthCheck(float value)
    {
        if (value <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}