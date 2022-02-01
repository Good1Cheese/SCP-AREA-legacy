using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(InteractionMarkEnablerDisabler))]
public class InteractionProvider : InteractableWithDelay
{
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private float _maxInteractionDistance;
    [SerializeField] private float _interactionSphereRadious;

    private RayProvider _rayProvider;
    private PickableInventoryEnablerDisabler _inventoryEnablerDisabler;
    private IInteractable _interactable;
    private bool _isDelayGoing;

    public Action UnInteractableFound { get; set; }
    public Action<Collider> InteractableFound { get; set; }
    public Action Interacted { get; set; }

    [Inject]
    private void Construct(RayProvider rayProvider, PickableInventoryEnablerDisabler inventoryEnablerDisabler)
    {
        _rayProvider = rayProvider;
        _inventoryEnablerDisabler = inventoryEnablerDisabler;
    }

    private void Update()
    {
        if (_isDelayGoing || _inventoryEnablerDisabler.IsActivated) { return; }

        Collider raycastHit = GetRayCastHit();

        if (raycastHit == null)
        {
            UnInteractableFound?.Invoke();
            return;
        }

        InteractableFound?.Invoke(raycastHit);
        TryInteract();
    }

    private Collider GetRayCastHit()
    {
        bool raycasted = Physics.SphereCast(_rayProvider.ProvideRay(), _interactionSphereRadious, out RaycastHit raycastHit, _maxInteractionDistance, _interactableLayerMask);

        Collider collider = raycastHit.collider;

        return raycasted && collider.TryGetComponent(out _interactable) ? collider : null;
    }

    public new void TryInteract()
    {
        if (!Input.GetButtonDown("Interaction")) { return; }

        base.TryInteract();
    }

    public override void Interact()
    {
        _interactable.Interact();
        Interacted?.Invoke();
    }
}