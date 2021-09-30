using System;
using UnityEngine;
using Zenject;

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
        if (!Physics.SphereCast(m_rayProvider.ProvideRay(),
                                m_radiousOfSphereInteraction,
                                out RaycastHit raycastHit,
                                m_maxInteractionDistance))
        {
            OnPlayerFindUnInteractable();
            return;
        }

        bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out m_interactable);

        if (!isHitObjectInteractable) { OnPlayerFindUnInteractable.Invoke(); return; }

        OnPlayerFindInteractable.Invoke(raycastHit.collider);

        if (Input.GetButtonDown("Interaction"))
        {
            m_interactable.Interact();
        }
    }

    void OnDestroy()
    {
        m_inventoryEnablerDisabler.OnInventoryButtonPressed -= SetActiveState;
    }

}
    