using UnityEngine;

[RequireComponent(typeof(RayProvider))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] RectTransform m_canvas;
    [SerializeField] Vector3 m_offset;
    [SerializeField] float m_maxInteractionDistance;
    [SerializeField] float m_radiousOfSphereInteraction;

    IInteractable m_interactable;
    RayProvider m_rayProvider;

    void Start()
    {
        m_rayProvider = GetComponent<RayProvider>();
    }

    void Update()
    {
        //if (m_interactable != null)
        //{
        //    m_interactable.ResetShader();
        //}

        Ray ray = m_rayProvider.ProvideRay();

        if (Physics.SphereCast(ray, m_radiousOfSphereInteraction, out RaycastHit raycastHit, m_maxInteractionDistance))
        {
            bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out m_interactable);

            if (!isHitObjectInteractable) { return; }


            m_canvas.position = raycastHit.collider.ClosestPoint(transform.position);

            if (Input.GetButtonDown("Interaction"))
            {
                m_interactable.Interact();
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
