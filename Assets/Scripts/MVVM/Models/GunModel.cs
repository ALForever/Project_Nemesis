using Assets.Scripts.CSharpClasses.Extensions;
using UnityEngine;
using Zenject;

public class GunModel : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private float m_offset;
    [SerializeField] private Transform m_bulletStartPoint;
    [SerializeField] private GameObject m_bullet;
    #endregion

    private InputController m_inputController;
    private GunInventorySystem m_gunInventorySystem;

    private float m_currentTimeBetweenShots;
    private GunScriptableObject m_currentGun;

    private AudioSource m_audioSource;

    #region Inject
    [Inject]
    public void Init(InputController inputController, GunInventorySystem gunInventorySystem)
    {
        m_inputController = inputController;
        m_gunInventorySystem = gunInventorySystem;
    }
    #endregion

    #region Unity Default Methods
    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        InitGunObject(m_gunInventorySystem.Gun);
        m_gunInventorySystem.OnGunChanged += InitGunObject;
    }

    private void Awake()
    {
        m_inputController.OnFireAction += FireGun;
        m_inputController.OnAimingAction += AimingGun;
    }

    private void OnDestroy()
    {
        m_inputController.OnFireAction -= FireGun;
        m_inputController.OnAimingAction -= AimingGun;
    }
    #endregion

    #region Input Controller Methods
    private void FireGun()
    {
        if (m_currentGun.IsNullObject())
        {
            return;
        }

        if (m_currentTimeBetweenShots <= 0)
        {
            GameObject bulletGameObject = Instantiate(m_bullet, m_bulletStartPoint.position, Quaternion.identity);
            if (bulletGameObject.TryGetComponent(out BulletModel bullet))
            {
                m_audioSource?.Play();
                bullet.InitBulletObject(m_currentGun.Damage, m_currentGun.Force, m_currentGun.MaxLife);
                bullet.SendBullet(m_bulletStartPoint.right); // Вектор направления будет завязан на пушке (сейчас right отлично подходит)
            }
            m_currentTimeBetweenShots = m_currentGun.MaxTimeBetweenShots;
        }
        else
        {
            m_currentTimeBetweenShots -= Time.deltaTime;
        }
    }

    private void AimingGun(Vector3 position)
    {
        position.z = Camera.main.nearClipPlane;
        position = Camera.main.ScreenToWorldPoint(position) - transform.position;
        float rotationZ = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ + m_offset);
    }
    #endregion

    #region GunController Methods
    private void InitGunObject(GunScriptableObject gunScriptable)
    {
        m_currentGun = gunScriptable;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_currentGun.Icon;
        m_audioSource.clip = m_currentGun.GunShootSound;
    }
    #endregion
}
