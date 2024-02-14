using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controllers
{
    public class InputSystemPlayerController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D m_rigidbody;
        [SerializeField]
        private Vector2 m_direction;
        [SerializeField]
        private float m_playerSpeed = 5;
        [SerializeField]
        private float m_dashSpeed = 24f;
        [SerializeField]
        private float m_dashingTime = 0.2f;


        // Start is called before the first frame update
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        void OnMove(InputValue inputValue)
        {
            m_direction = inputValue.Get<Vector2>();
            m_direction.Normalize();
        }

        void OnDash()
        {
            StartCoroutine(DashCoroutine());
            m_rigidbody.MovePosition(new Vector2(0f, 0f));
            transform.position = new Vector3(0, 0, 0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            m_rigidbody.MovePosition(m_rigidbody.position + m_playerSpeed * Time.deltaTime * m_direction);
        }

        private bool m_isDashing = false;
        IEnumerator DashCoroutine()
        {
            if (m_isDashing)
            {
                yield break;
            } 

            m_isDashing = true;
            m_rigidbody.velocity = new Vector2(m_dashSpeed, 0);

            yield return new WaitForSeconds(m_dashingTime);

            m_rigidbody.velocity = new Vector2(0, 0);

            m_isDashing = false;
        }
    }
}
