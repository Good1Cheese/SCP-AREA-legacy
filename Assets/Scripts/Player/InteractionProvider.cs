using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(InteractionMarkEnablerDisabler))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] float m_maxInteractionDistance;
    [SerializeField] float m_radiousOfSphereInteraction;

    [Inject] readonly RayProvider m_rayProvider;
    [Inject] readonly InventoryEnablerDisabler m_inventoryEnablerDisabler;

    IInteractable m_interactable;

    public Action OnPlayerFindUnInteractable { get; set; }
    public Action<Collider> OnPlayerFindInteractable { get; set; }

    void Start()
    {
        m_inventoryEnablerDisabler.OnInventoryButtonPressed += SetActiveState;
    }

    void SetActiveState()
    {
        enabled = !enabled;
    }

    void Update()
    {
        RaycastHit? raycastHit = GetInteractableObject(Physics.SphereCastAll(m_rayProvider.ProvideRay(),
                                m_radiousOfSphereInteraction,
                                m_maxInteractionDistance));

        if (raycastHit == null) { return; }

        OnPlayerFindInteractable.Invoke(raycastHit.Value.collider);

        m_interactable = raycastHit.Value.collider.gameObject.GetComponent<IInteractable>();

        if (Input.GetButtonDown("Interaction"))
        {
            m_interactable.Interact();
        }
    }


    public RaycastHit? GetInteractableObject(RaycastHit[] raycastHits)
    {
        RaycastHit? interactableObject = null;

        if (raycastHits == null)
        {
            OnPlayerFindUnInteractable();
            return null;
        }

        for (int i = 0; i < raycastHits.Length; i++)
        {
            bool isHitObjectInteractable = raycastHits[i].collider.gameObject.TryGetComponent(out m_interactable);

            if (!isHitObjectInteractable) { continue; }

            if (interactableObject == null) { interactableObject = raycastHits[i]; continue; }

            if (raycastHits[i].distance < interactableObject.Value.distance)
            {
                interactableObject = raycastHits[i];
            }
        }

        return interactableObject;
    }

    void OnDestroy()
    {
        m_inventoryEnablerDisabler.OnInventoryButtonPressed -= SetActiveState;
    }

}
