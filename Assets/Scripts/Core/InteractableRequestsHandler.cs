using System.Collections;

public class InteractableRequestsHandler : CoroutineUser
{
    private InteractableWithDelay _interactable;

    public void Handle(InteractableWithDelay interactable)
    {
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