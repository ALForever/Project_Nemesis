using UnityEngine;
using Zenject;

public class DroppedGunModel : MonoBehaviour
{
    [SerializeField] private float m_timeToDisposeGun;

    private GunScriptableObject m_gunScriptableObject;

    private InputController m_inputController;
    private GunInventorySystem m_inventorySystem;

    private AudioSource m_audioSource;

    private bool m_canInterract;
    private bool m_inBeforeDestroyActions = false;

    private float m_currentDropOnGroundTime;

    [Inject]
    public void Init(InputController inputController, GunInventorySystem gunInventorySystem)
    {
        m_inputController = inputController;
        m_inventorySystem = gunInventorySystem;
    }
    
    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_inputController.OnInterractAction += InterractAction;
    }

    void Update()
    {
        m_currentDropOnGroundTime += Time.deltaTime;

        if (m_currentDropOnGroundTime >= m_timeToDisposeGun)
        {
            Destroy(gameObject);
        }
    }

    #region Collider Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerModel>(out _))
        {
            m_canInterract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerModel>(out _))
        {
            m_canInterract = false;
        }
    }
    #endregion

    private void InterractAction()
    {
        if (!m_canInterract || m_inBeforeDestroyActions)
        {
            return;
        }

        if (!m_inventorySystem.TryAddGunBasedOnLevelSystem(m_gunScriptableObject))
        {
            return;
        }
        BeforeDestroyAction();
        Destroy(gameObject, m_audioSource.clip.length);
    }

    private void BeforeDestroyAction()
    {
        m_audioSource?.Play();
        m_inBeforeDestroyActions = true;

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void AfterInstantinate(GunScriptableObject gunScriptableObject)
    {
        m_gunScriptableObject = gunScriptableObject;
        InitGunObject();
    }

    private void InitGunObject()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_gunScriptableObject.Icon;
    }
}
