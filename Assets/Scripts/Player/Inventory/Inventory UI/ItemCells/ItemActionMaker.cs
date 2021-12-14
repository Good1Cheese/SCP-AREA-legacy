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

    public bool IsGoing { get; set; }
    public Action ActionStarted { get; set; }

    public void StartItemAction(WaitForSeconds timeout, AudioClip actionSound)
    {
        Debug.Log("Начато непрерываемое действие");

        PlaySound(actionSound);
        _wearableItemActivator.StartCoroutine(DoAction(timeout));
    }

    public void StartInterruptingItemAction(CoroutineUser coroutineUser, AudioClip actionSound)
    {
        Debug.Log("Начато прерывание действие");

        PlaySound(actionSound);
        _currentItemAction = coroutineUser;
        ActionStarted?.Invoke();
    }

    private void PlaySound(AudioClip actionSound)
    {
        _slotAudio.clip = actionSound;
        _slotAudio.Play();
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
        ActionStarted?.Invoke();
            
        if (_currentItemAction == null) { return; }

        _currentItemAction.StopAction();
        _currentItemAction = null;
    }

    public void StartEmptyItemActionWithAudioStop()
    {
        Debug.Log("Прерывание действия");

        _slotAudio.Stop();
        StartEmptyItemAction();
    }
}