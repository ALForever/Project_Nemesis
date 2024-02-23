using UnityEngine;
using Zenject;

public class GunController : MonoBehaviour
{
    #region SerializeField
    [Inject] private InputController m_inputController;
    [SerializeField] private float m_offset;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private Transform m_bulletStartPoint;
    [SerializeField] private float m_maxTimeBetweenShots = 0.0001f;
    #endregion

    private float m_currentTimeBetweenShots;

    #region Unity Default Methods
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
        if (m_currentTimeBetweenShots <= 0)
        {
            GameObject bullet = Instantiate(m_bullet, m_bulletStartPoint.position, Quaternion.identity);
            if (bullet.TryGetComponent(out BulletController bulletAction))
            {
                // Вектор направления будет завязан на пушке (сейчас right отлично подходит)
                bulletAction.SendBullet(m_bulletStartPoint.right);
            }
            m_currentTimeBetweenShots = m_maxTimeBetweenShots;
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
    #endregion
}
