using UnityEngine;

[RequireComponent(typeof(RayProvider))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] float m_maxInteractionDistance;
    [SerializeField] float m_radiousOfSphereInteraction;
    RayProvider m_rayProvider;

    void Start()
    {
        m_rayProvider = GetComponent<RayProvider>();
    }

    void Update()
    {
        Ray ray = m_rayProvider.ProvideRay();

        if (Physics.SphereCast(ray, m_radiousOfSphereInteraction, out RaycastHit raycastHit, m_maxInteractionDistance))
        {
            bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out IInteractable interactable);

            if (!isHitObjectInteractable) { return; }

            if (Input.GetButtonDown("Interaction"))
            {
                interactable.Interact();
            }
        }

    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Debug.DrawLine(origin, origin + Toorigin * m_currentHitDisnace);
    //    Gizmos.DrawWireSphere(origin + Toorigin * m_currentHitDisnace, m_radiousOfSphereInteraction);
    //}
}
