using Assets.Scripts.CSharpClasses.Extensions;
using Assets.Scripts.CSharpClasses.Interfaces;
using Assets.Scripts.CSharpClasses.Reactive;
using UnityEngine;
using Zenject;

public class PlayerModel : MonoBehaviour, ICharacter
{
    #region SerializeField
    [SerializeField] private PlayerScriptableObject m_playerScriptableObject;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    #endregion

    #region Fileds form Inject
    private InputController m_inputController;
    private LevelSystem m_levelSystem;
    private GunInventorySystem m_gunInventorySystem;
    #endregion

    private Rigidbody2D m_rigidbody;

    private Vector2 m_direction;
    private float m_currentDashDistance = 0f;
    private Vector2 m_dashDirection;

    public ReactiveProperty<float> MaxHealth { get; set; } = new ReactiveProperty<float>();
    public ReactiveProperty<float> CurrentHealth { get; set; } = new ReactiveProperty<float>();

    #region Inject
    [Inject]
    public void Init(InputController inputController, LevelSystem levelSystem, GunInventorySystem gunInventorySystem)
    {
        m_inputController = inputController;
        m_levelSystem = levelSystem;
        m_gunInventorySystem = gunInventorySystem;
    }
    #endregion

    #region Unity Default Methods
    private void Awake()
    {
        m_inputController.OnMoveAction += MovePlayer;
        m_inputController.OnDashAction += DashPlayer;
        m_inputController.OnAimingAction += Aiming;
        m_inputController.OnChangeGunAction += ChangeGun;

        m_levelSystem.OnNextLevel += InitPlayerObject;

        CurrentHealth.Subscribe(x => HealthCheck(x));
    }

    private void OnDestroy()
    {
        m_inputController.OnMoveAction -= MovePlayer;
        m_inputController.OnDashAction -= DashPlayer;
        m_inputController.OnAimingAction -= Aiming;
        m_inputController.OnChangeGunAction -= ChangeGun;

        m_levelSystem.OnNextLevel += InitPlayerObject;

        CurrentHealth.Unsubscribe(x => HealthCheck(x));
    }

    void Start()
    {
        InitPlayerObject();
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

    private void Aiming(Vector3 position)
    {
        position = Camera.main.ScreenToWorldPoint(position);
        m_spriteRenderer.flipX = position.x < transform.position.x;
    }

    private void ChangeGun()
    {
        m_gunInventorySystem.GoToNextGun();
    }
    #endregion

    #region Get Vector Methods
    private Vector2 GetMoveVector()
    {
        return m_rigidbody.position.GetMovementVector(m_playerScriptableObject.PlayerSpeed, m_direction);
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
    public void InitPlayerObject(PlayerScriptableObject playerScriptableObject)
    {
        m_playerScriptableObject = playerScriptableObject;
        InitPlayerObject();
    }

    public void InitPlayerObject()
    {
        MaxHealth.Value = m_playerScriptableObject.MaxHealth; //Порядок важен, сначала макс потом карент, иначе слайдер будет в нуле
        CurrentHealth.Value = m_playerScriptableObject.MaxHealth;
    }

    private void HealthCheck(float value)
    {
        if (value <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}