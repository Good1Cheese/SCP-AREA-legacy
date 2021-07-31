using System;
using UnityEngine;
using Zenject;

public class InteractionProvider : MonoBehaviour
{
    [SerializeField] float m_maxInteractionDistance;
    [SerializeField] float m_radiousOfSphereInteraction;
    [Inject] readonly RayProvider m_rayProvider;

    IInteractable m_interactable;

    public Action OnPlayerFindUnInteractable;
    public Action<Collider> OnPlayerFindInteractable;

    void Update()
    {
        if (Physics.SphereCast(
            m_rayProvider.ProvideRay(), m_radiousOfSphereInteraction, out RaycastHit raycastHit, m_maxInteractionDistance))
        {
            bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out m_interactable);

            if (!isHitObjectInteractable) { OnPlayerFindUnInteractable.Invoke(); return; }

            OnPlayerFindInteractable.Invoke(raycastHit.collider);

            if (Input.GetButtonDown("Interaction"))
            {
                m_interactable.Interact();
            }
        }
    }

}
    