using UnityEngine;
using Zenject;

public class PlayerMarkEnablerDisabler : MonoBehaviour
{
    [SerializeField] RectTransform m_markCanvas;
    [Inject] readonly Transform m_playerTransform;

    InteractionProvider m_interactionProvider;
    GameObject m_canvasGameObject;

    void Awake()
    {
        m_interactionProvider = GetComponent<InteractionProvider>();
        m_canvasGameObject = m_markCanvas.gameObject;
    }

    void Start()
    {
        m_interactionProvider.OnPlayerFindInteractable += ActivateMark;
        m_interactionProvider.OnPlayerFindUnInteractable += DisableMark;
    }

    public void ActivateMark(Collider collider)
    {
        m_canvasGameObject.SetActive(true);
        m_markCanvas.rotation = Quaternion.LookRotation(m_playerTransform.position - m_markCanvas.position);
        m_markCanvas.position = collider.ClosestPoint(m_playerTransform.position);
    }

    private void DisableMark()
    {
        m_canvasGameObject.SetActive(false);
    }
}