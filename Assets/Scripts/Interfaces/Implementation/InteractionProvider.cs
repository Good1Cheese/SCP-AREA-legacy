using UnityEngine;

[RequireComponent(typeof(RayProvider))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] float m_maxInteractionDistance;
    RayProvider m_rayProvider;

    void Start()
    {
        m_rayProvider = GetComponent<RayProvider>();
    }

    void Update()
    {
        Ray ray = m_rayProvider.ProvideRay();
        if (Physics.Raycast(ray, out RaycastHit hitInfo, m_maxInteractionDistance))
        {
            bool isHitObjectInteractable = hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable);
            if (!isHitObjectInteractable) { return; }

            if (Input.GetButtonDown("Interaction"))
            {
                interactable.Interact();
            }
        }

    }
}
