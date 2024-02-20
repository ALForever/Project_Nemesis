using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_bulletForce = 1;
    [SerializeField]
    private float m_maxLife = 15;

    private Rigidbody2D m_rigidbody;
    private float m_life = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_life += Time.deltaTime;

        if (m_life >= m_maxLife)
        {
            DestroyBullet();
        }
    }

    public void SendBullet(Vector2 direction)
    {
        if (m_rigidbody == null)
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
        m_rigidbody.AddForce(direction * m_bulletForce);
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject); //Уничтожение объекта
    }
}
