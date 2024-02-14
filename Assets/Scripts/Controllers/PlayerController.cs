using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    [SerializeField]
    private float m_movementSpeed = 2;
    [SerializeField]
    private float m_dashValue = 2;
    [SerializeField]
    private GameObject m_body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
        //HandlePlayerOrientation();
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetButton(HORIZONTAL_INPUT))
        {
            transform.position = ReturnPositionFromInput(HORIZONTAL_INPUT);
        }

        if (Input.GetButton(VERTICAL_INPUT))
        {
            transform.position = ReturnPositionFromInput(VERTICAL_INPUT);
        }
    }

    private void HandlePlayerOrientation()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(m_body.transform.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        m_body.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private Vector3 ReturnPositionFromInput(string buttonName)
    {
        float dashValue = Input.GetKeyDown(KeyCode.Space) ? m_dashValue : 0;

        int direction = Input.GetAxis(buttonName) < 0 ? -1 : 1;

        if (dashValue > 0)
        {
            dashValue *= direction;
        }

        float deltaValue = Time.deltaTime * direction * m_movementSpeed;

        float x = buttonName == HORIZONTAL_INPUT ? transform.position.x + deltaValue + dashValue : transform.position.x;
        float y = buttonName == VERTICAL_INPUT ? transform.position.y + deltaValue + dashValue : transform.position.y;

        return new Vector3(x, y, transform.position.z);
    }
}
