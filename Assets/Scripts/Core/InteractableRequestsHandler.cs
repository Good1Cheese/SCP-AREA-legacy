using System.Collections;

public class InteractableRequestsHandler : CoroutineUser
{
    private InteractableWithDelay _interactable;

    public void Handle(InteractableWithDelay interactable)
    {
        if (interactable.CanNotInteract) { return; }

        _interactable = interactable;
        StartWithoutInterrupt();
    }

    protected override IEnumerator Coroutine()
    {
        yield return _interactable.TimeoutBeforeInteraction.TimeOut;

        _interactable.Interact();

        yield return _interactable.TimeoutAfterInteraction.TimeOut;
        IsCoroutineGoing = false;
    }
}