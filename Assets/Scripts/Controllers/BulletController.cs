using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private float m_bulletForce = 1;
    [SerializeField] private float m_maxLife = 15;
    #endregion

    private Rigidbody2D m_rigidbody;
    private float m_life = 0;

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
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        DestroyBullet();
    }
    #endregion

    #region Bullet Controller Methods
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
