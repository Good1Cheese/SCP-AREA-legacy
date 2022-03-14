using UnityEngine;
using Zenject;

public abstract class InteractableWithDelay : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionTimeout _timeoutBeforeInteraction;
    [SerializeField] private InteractionTimeout _timeoutAfterInteraction;

    private InteractableRequestsHandler _interactableRequestsHandler;
    protected PickableInventoryToggler _pickableInventoryToggler;
    protected AmmoUIEnablerDisabler _ammoUIEnablerDisabler;
    protected PauseMenuToggler _pauseMenuToggler;

    [Inject]
    private void Construct(PickableInventoryToggler pickableInventoryToggler,
                           AmmoUIEnablerDisabler ammoUIEnablerDisabler,
                           PauseMenuToggler pauseMenuToggler,
                           InteractableRequestsHandler interactableRequestsHandler)
    {
        _pickableInventoryToggler = pickableInventoryToggler;
        _ammoUIEnablerDisabler = ammoUIEnablerDisabler;
        _pauseMenuToggler = pauseMenuToggler;
        _interactableRequestsHandler = interactableRequestsHandler;
    }

    public virtual bool CanNotInteract => _pickableInventoryToggler.IsToggled
                                        || _pauseMenuToggler.IsToggled
                                        || _ammoUIEnablerDisabler.IsActivated;

    public InteractionTimeout TimeoutAfterInteraction => _timeoutAfterInteraction;
    public InteractionTimeout TimeoutBeforeInteraction => _timeoutBeforeInteraction;


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