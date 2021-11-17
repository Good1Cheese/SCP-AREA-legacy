using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(InteractionMarkEnablerDisabler))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] private LayerMask _itemsLayerMask;
    [SerializeField] private float _maxInteractionDistance;
    [SerializeField] private float _radiousOfSphereInteraction;
    [SerializeField] private float _delayAfterInteraction;

    [Inject] private readonly RayProvider _rayProvider;
    [Inject] private readonly InventoryEnablerDisabler _inventoryEnablerDisabler;
    [Inject] private readonly GameObject _playerGameObject;
    private IInteractable _interactable;
    private bool _isDelayGoing;
    private WaitForSeconds _timeoutAfterInteraction;

    public Action OnPlayerFindUnInteractable { get; set; }
    public Action<Collider> OnPlayerFindInteractable { get; set; }

    private void Start()
    {
        _inventoryEnablerDisabler.OnInventoryEnabledDisabled += SetActiveState;
        _timeoutAfterInteraction = new WaitForSeconds(_delayAfterInteraction);
    }

    private void SetActiveState()
    {
        enabled = !enabled;
    }

    private void Update()
    {
        if (_isDelayGoing) { return; }

        RaycastHit[] raycastHits = Physics.SphereCastAll(_rayProvider.ProvideRay(),
                                                         _radiousOfSphereInteraction,
                                                         _maxInteractionDistance,
                                                         _itemsLayerMask);

        Collider raycastHit = GetInteractableObject(raycastHits);

        if (raycastHit == null)
        {
            OnPlayerFindUnInteractable?.Invoke();
            return;
        }

        OnPlayerFindInteractable?.Invoke(raycastHit);

        if (!Input.GetButtonDown("Interaction")) { return; }

        StartCoroutine(StartInteractionDelay());
        _interactable.Interact();
    }

    private Collider GetInteractableObject(RaycastHit[] raycastHits)
    {
        if (raycastHits == null)
        {
            return null;
        }

        return raycastHits.LastOrDefault(hit => hit.collider.gameObject.TryGetComponent(out _interactable)).collider;
    }

    private IEnumerator StartInteractionDelay()
    {
        OnPlayerFindUnInteractable?.Invoke();
        _isDelayGoing = true;

        yield return _timeoutAfterInteraction;

        _isDelayGoing = false;
    }

    private void OnDestroy()
    {
        _inventoryEnablerDisabler.OnInventoryEnabledDisabled -= SetActiveState;
    }

}
