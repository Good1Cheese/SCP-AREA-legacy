using UnityEngine;
using Zenject;

public abstract class InteractableWithDelay : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionTimeout _timeoutBeforeInteraction;
    [SerializeField] private InteractionTimeout _timeoutAfterInteraction;

    private InteractableRequestsHandler _interactableRequestsHandler;

    public InteractionTimeout TimeoutAfterInteraction => _timeoutAfterInteraction;
    public InteractionTimeout TimeoutBeforeInteraction => _timeoutBeforeInteraction;

    [Inject]
    private void Construct(InteractableRequestsHandler interactableRequestsHandler)
    {
        _interactableRequestsHandler = interactableRequestsHandler;
    }

    protected void Awake()
    {
        _timeoutAfterInteraction.CreateTimeOut();
        _timeoutBeforeInteraction.CreateTimeOut();
    }

    public void TryInteract()
    {
        _interactableRequestsHandler.Handle(this);
    }

    public abstract void Interact();
}