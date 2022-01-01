using System.Collections;
using UnityEngine;
using Zenject;

public class ItemActionCreator : MonoBehaviour
{
    private CoroutineUser _currentItemAction;
    private bool _isSoundInterrupting = true;

    public bool IsGoing { get; set; }

    [Inject(Id = "ItemsAudio")] public AudioSource SlotAudio { get; set; }

    public void StartItemAction(WaitForSeconds timeout, AudioClip actionSound, bool isSoundInterrupting = true)
    {
        PlaySound(actionSound);
        _isSoundInterrupting = isSoundInterrupting;
        StartCoroutine(DoAction(timeout));
    }

    public void StartInterruptingItemAction(CoroutineUser coroutineUser, AudioClip actionSound)
    {
        PlaySound(actionSound);
        _isSoundInterrupting = true;
        _currentItemAction = coroutineUser;
        RaiseActionStartedEvent();
    }

    private void PlaySound(AudioClip actionSound)
    {
        SlotAudio.clip = actionSound;
        SlotAudio.Play();
    }

    private IEnumerator DoAction(WaitForSeconds timeout)
    {
        IsGoing = true;

        StartEmptyItemAction();
        yield return timeout;

        IsGoing = false;
    }

    public void StartEmptyItemAction()
    {
        RaiseActionStartedEvent();

        if (_currentItemAction == null) { return; }

        _currentItemAction.Stop();
        _currentItemAction = null;
    }

    public void StartEmptyItemActionWithAudioStop()
    {
        StartEmptyItemAction();

        if (!_isSoundInterrupting) { return; }

        SlotAudio.Stop();
    }

    private static void RaiseActionStartedEvent()
    {
        WearableItemActivator currentItemActivator = WearableSlot.CurrentItemActivator;

        if (currentItemActivator == null) { return; }

        currentItemActivator.Slot.ActionStarted?.Invoke();
    }
}