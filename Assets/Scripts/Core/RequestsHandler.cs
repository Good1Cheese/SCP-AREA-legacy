using Zenject;

public abstract class RequestsHandler : CoroutineUser
{
    private PickableInventoryToggler _pickableInventoryToggler;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;
    private PauseMenuToggler _pauseMenuToggler;

    [Inject]
    private void Construct(PickableInventoryToggler pickableInventoryToggler,
                       AmmoUIEnablerDisabler ammoUIEnablerDisabler,
                       PauseMenuToggler pauseMenuToggler)
    {
        _pickableInventoryToggler = pickableInventoryToggler;
        _ammoUIEnablerDisabler = ammoUIEnablerDisabler;
        _pauseMenuToggler = pauseMenuToggler;
    }

    public bool CanNotHandle => _pickableInventoryToggler.IsToggled
        || _pauseMenuToggler.IsToggled
        || _ammoUIEnablerDisabler.IsActivated;
}