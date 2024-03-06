using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    #region Inject Field
    [Inject] private readonly UIController m_uIController;
    [Inject] private readonly InputController m_inputController;
    #endregion

    #region SerializeField
    [SerializeField] private PlayerScriptableObject m_playerScriptableObject;
    #endregion

    private Rigidbody2D m_rigidbody;
    private Vector2 m_direction;
    private float m_currentDashDistance = 0f;
    private Vector2 m_dashDirection;
    private float m_currentHealth;

    public float CurrentHealth
    {
        get => m_currentHealth;
        set
        {
            m_uIController.SetPlayerHealthValue(value);
            if (value <= 0)
            {
                DestroyPlayer();
                m_inputController.enabled = false;
                m_uIController.ShowPlayerDeathText();
                return;
            }
            m_currentHealth = value;
        }
    }

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
        CurrentHealth = m_playerScriptableObject.MaxHealth;
        m_uIController.SetMaxHealth(CurrentHealth);
        m_rigidbody = GetComponent<Rigidbody2D>();
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
    private void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }
    #endregion
}

