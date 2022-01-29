using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(InteractionMarkEnablerDisabler))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private float _maxInteractionDistance;
    [SerializeField] private float _interactionSphereRadious;
    [SerializeField] private float _delayAfterInteraction;

    private RayProvider _rayProvider;
    private PickableInventoryEnablerDisabler _inventoryEnablerDisabler;
    private Interactable _interactable;
    private bool _isDelayGoing;
    private WaitForSeconds _timeoutAfterInteraction;

    public Action UnInteractableFound { get; set; }
    public Action<Collider> InteractableFound { get; set; }
    public Action Interacted { get; set; }

    [Inject]
    private void Construct(RayProvider rayProvider, PickableInventoryEnablerDisabler inventoryEnablerDisabler)
    {
        _rayProvider = rayProvider;
        _inventoryEnablerDisabler = inventoryEnablerDisabler;
    }

    private void Awake()
    {
        _timeoutAfterInteraction = new WaitForSeconds(_delayAfterInteraction);
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
        Interact();
    }

    private Collider GetRayCastHit()
    {
        bool raycasted = Physics.SphereCast(_rayProvider.ProvideRay(), _interactionSphereRadious, out RaycastHit raycastHit, _maxInteractionDistance, _interactableLayerMask);

        Collider collider = raycastHit.collider;

        return raycasted && collider.TryGetComponent(out _interactable) ? collider : null;
    }

    private void Interact()
    {
        if (!Input.GetButtonDown("Interaction")) { return; }

        _interactable.Interact();
        Interacted?.Invoke();
        StartCoroutine(StartInteractionDelay());
    }

    private IEnumerator StartInteractionDelay()
    {
        UnInteractableFound?.Invoke();
        _isDelayGoing = true;

        yield return _timeoutAfterInteraction;

        _isDelayGoing = false;
    }
}