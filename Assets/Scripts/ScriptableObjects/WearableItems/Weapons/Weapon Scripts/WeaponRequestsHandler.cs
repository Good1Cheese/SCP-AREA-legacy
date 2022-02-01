using System.Collections;
using UnityEngine;

public class WeaponRequestsHandler : CoroutineUser
{
    private WaitForSeconds _timeout;
    private IInteractable _interactable;

    public void Handle(IInteractable interactable, WaitForSeconds timeout)
    {
        _interactable = interactable;
        _timeout = timeout;
        StartWithoutInterrupt();
    }

    protected override IEnumerator Coroutine()
    {
        print($"Weapon Action Started {_interactable}");
        _interactable.Interact();

        yield return _timeout;
        IsCoroutineGoing = false;
        print($"Weapon Action Ended {_interactable}");
    }
}