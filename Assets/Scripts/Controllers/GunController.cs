using Assets.Scripts.Extensions;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GunController : MonoBehaviour
{
    #region Inject Field
    [Inject] private readonly InputController m_inputController;
    #endregion

    #region SerializeField
    [SerializeField] private float m_offset;
    [SerializeField] private Transform m_bulletStartPoint;
    [SerializeField] private List<GunScriptableObject> m_gunsScriptableObject;
    [SerializeField] private GameObject m_bullet;
    #endregion

    private float m_currentTimeBetweenShots;
    private GunScriptableObject m_currentGun;
    private int m_currentGunIndex;

    #region Unity Default Methods
    private void Start()
    {
        if (m_gunsScriptableObject.IsNullOrEmptyList())
        {
            return;
        }
        InitGunObject(m_gunsScriptableObject[0]);
    }

    private void Awake()
    {
        m_inputController.OnFireAction += FireGun;
        m_inputController.OnAimingAction += AimingGun;
        m_inputController.OnChangeGunAction += ChangeGun;
    }

    private void OnDestroy()
    {
        m_inputController.OnFireAction -= FireGun;
        m_inputController.OnAimingAction -= AimingGun;
        m_inputController.OnChangeGunAction -= ChangeGun;
    }
    #endregion

    #region Input Controller Methods
    private void FireGun()
    {
        if (m_currentGun == null)
        {
            return;
        }

        if (m_currentTimeBetweenShots <= 0)
        {
            GameObject bulletGameObject = Instantiate(m_bullet, m_bulletStartPoint.position, Quaternion.identity);
            if (bulletGameObject.TryGetComponent(out BulletController bulletController))
            {
                bulletController.InitBulletObject(m_currentGun.Damage, m_currentGun.Force, m_currentGun.MaxLife);
                bulletController.SendBullet(m_bulletStartPoint.right); // Вектор направления будет завязан на пушке (сейчас right отлично подходит)
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

    private void ChangeGun()
    {
        if (m_gunsScriptableObject.IsNullOrEmptyList())
        {
            return;
        }

        m_currentGunIndex++;
        if (m_currentGunIndex >= m_gunsScriptableObject.Count)
        {
            m_currentGunIndex = 0;
        }
        InitGunObject(m_gunsScriptableObject[m_currentGunIndex]);
    }
    #endregion

    #region GunController Methods
    private void InitGunObject(GunScriptableObject gunScriptable)
    {
        m_currentGun = gunScriptable;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_currentGun.Icon;
    }
    #endregion
}
