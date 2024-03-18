using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BulletModel : MonoBehaviour
{
    [SerializeField] private float m_offset;

    private Rigidbody2D m_rigidbody;
    private float m_bulletDamage;
    private float m_bulletForce;
    private float m_maxLifeTime;
    private float m_currentLifeTime;

    public float BulletDamage => m_bulletDamage;

    #region Unity Default Methods
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_currentLifeTime += Time.deltaTime;

        if (m_currentLifeTime >= m_maxLifeTime)
        {
            DestroyBullet();
        }
    }
    #endregion

    #region Collider Methods

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerModel>())
        {
            return;
        }        
        DestroyBullet();
    }
    #endregion

    #region Bullet Controller Methods
    public void InitBulletObject(float bulletDamage, float bulletForce, float maxLife, float rotationZ)
    {
        m_bulletDamage = bulletDamage;
        m_bulletForce = bulletForce;
        m_maxLifeTime = maxLife;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ + m_offset);
    }

    public void SendBullet(Vector2 direction)
    {
        if (m_rigidbody.IsUnityNull())
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
        m_rigidbody.AddForce(direction * m_bulletForce);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    #endregion
}
