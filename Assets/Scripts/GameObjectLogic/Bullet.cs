using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float m_bulletForce;
    private float m_maxLife;
    private Rigidbody2D m_rigidbody;
    private float m_life;
    private float m_bulletDamage;

    public float BulletDamage => m_bulletDamage;

    #region Unity Default Methods
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_life += Time.deltaTime;

        if (m_life >= m_maxLife)
        {
            DestroyBullet();
        }
    }
    #endregion

    #region Collider Methods

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            return;
        }        
        DestroyBullet();
    }
    #endregion

    #region Bullet Controller Methods
    public void InitBulletObject(float bulletDamage, float bulletForce, float maxLife)
    {
        m_bulletDamage = bulletDamage;
        m_bulletForce = bulletForce;
        m_maxLife = maxLife;
    }

    public void SendBullet(Vector2 direction)
    {
        if (m_rigidbody == null)
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