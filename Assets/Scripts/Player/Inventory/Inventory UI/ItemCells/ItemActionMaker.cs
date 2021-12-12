using System;
using System.Collections;
using UnityEngine;

public class ItemActionMaker
{
    private readonly WearableItemActivator _wearableItemActivator;
    private readonly AudioSource _slotAudio;
    private CoroutineUser _currentItemAction;

    public ItemActionMaker(WearableItemActivator wearableItemActivator, AudioSource slotAudio)
    {
        _wearableItemActivator = wearableItemActivator;
        _slotAudio = slotAudio;
    }

    public bool IsItemActionGoing { get; set; }
    public Action OnNewActionStarted { get; set; }

    public void StartItemAction(WaitForSeconds timeout, AudioClip actionSound)
    {
        PlaySound(actionSound);
        _wearableItemActivator.StartCoroutine(DoAction(timeout));
    }

    public void StartInterruptingItemAction(CoroutineUser coroutineUser, AudioClip actionSound)
    {
        PlaySound(actionSound);
        _currentItemAction = coroutineUser;
        OnNewActionStarted?.Invoke();
    }

    private void PlaySound(AudioClip actionSound)
    {
        _slotAudio.clip = actionSound;
        _slotAudio.Play();
    }

    private IEnumerator DoAction(WaitForSeconds timeout)
    {
        IsItemActionGoing = true;

        StartEmptyItemAction();
        yield return timeout;

        IsItemActionGoing = false;
    }

    public void StartEmptyItemAction()
    {
        OnNewActionStarted?.Invoke();

        if (_currentItemAction == null) { return; }

        _currentItemAction.StopAction();
        _currentItemAction = null;
    }

    public void StartEmptyItemAction2()
    {
        _slotAudio.Stop();
        StartEmptyItemAction();
    }
}