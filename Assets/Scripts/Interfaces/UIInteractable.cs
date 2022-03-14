public abstract class UIInteractable : InteractableWithDelay
{
    public override bool CanNotInteract => _pauseMenuToggler.IsToggled
                                           || _ammoUIEnablerDisabler.IsActivated;
}