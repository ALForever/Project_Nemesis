using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    #region Inject Field
    [Inject] private readonly InputController m_inputController;
    #endregion

    #region SerializeField
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Vector2 m_direction;
    [SerializeField] private float m_playerSpeed = 5;
    [SerializeField] private float m_dashDistance = 5f;
    #endregion

    private float m_currentDashDistance = 0f;
    private Vector2 m_dashDirection;

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
        m_currentDashDistance = m_dashDistance;
    }
    #endregion

    #region Get Vector Methods
    private Vector2 GetMoveVector()
    {
        return m_rigidbody.position + m_playerSpeed * Time.deltaTime * m_direction;
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
}

