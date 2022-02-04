using System.Collections;

public class InteractableRequestsHandler : RequestsHandler
{
    private InteractableWithDelay _interactable;

    public void Handle(InteractableWithDelay interactable)
    {
        if (CanNotHandle) { return; }

        _interactable = interactable;
        StartWithoutInterrupt();
    }

    protected override IEnumerator Coroutine()
    {
        print($"Action Started {_interactable}");

        yield return _interactable.TimeoutBeforeInteraction.TimeOut;

        _interactable.Interact();

        yield return _interactable.TimeoutAfterInteraction.TimeOut;
        IsCoroutineGoing = false;

        print($"Action Ended {_interactable}");
    }
}