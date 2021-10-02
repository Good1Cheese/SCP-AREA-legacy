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

        if (raycastHit == null) 
        {
            OnPlayerFindUnInteractable.Invoke(); 
            return;
        }

        OnPlayerFindInteractable.Invoke(raycastHit?.collider);

        if (Input.GetButtonDown("Interaction"))
        {
            m_interactable.Interact();
        }
    }


    public RaycastHit? GetInteractableObject(RaycastHit[] raycastHits)
    {
        if (raycastHits == null)
        {
            OnPlayerFindUnInteractable();
            return null;
        }

        for (int i = 0; i < raycastHits.Length; i++)
        {
            bool isHitObjectInteractable = raycastHits[i].collider.gameObject.TryGetComponent(out m_interactable);

            if (isHitObjectInteractable) 
            {
                return raycastHits[i];
            }
        }

        return null;
    }

    void OnDestroy()
    {
        m_inventoryEnablerDisabler.OnInventoryButtonPressed -= SetActiveState;
    }

}
