using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class ItemActionCreator : MonoBehaviour
{
    [Inject(Id = "ItemsAudio")] private readonly AudioSource _slotAudio;

    private CoroutineUser _currentItemAction;
    private bool _isSoundInterrupting = true;

    public bool IsGoing { get; set; }

    public void StartItemAction(WaitForSeconds timeout, AudioClip actionSound, bool isSoundInterrupting = true)
    {
        Debug.Log("Начато непрерываемое действие");

        PlaySound(actionSound);
        _isSoundInterrupting = isSoundInterrupting;
         StartCoroutine(DoAction(timeout));
    }

    public void StartInterruptingItemAction(CoroutineUser coroutineUser, AudioClip actionSound)
    {
        Debug.Log("Начато прерывание действие");

        PlaySound(actionSound);
        _isSoundInterrupting = true;
        _currentItemAction = coroutineUser;
        RaiseActionStartedEvent();
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
        RaiseActionStartedEvent();

        if (_currentItemAction == null) { return; }

        _currentItemAction.StopAction();
        _currentItemAction = null;
    }

    public void StartEmptyItemActionWithAudioStop()
    {
        Debug.Log("Прерывание действия");

        StartEmptyItemAction();

        if (!_isSoundInterrupting) { return; }

        _slotAudio.Stop();
    }

    private static void RaiseActionStartedEvent()
    {
        WearableItemActivator currentItemActivator = WearableSlot.CurrentItemActivator;

        if (currentItemActivator == null) { return; }

        print("RaisedEvent on "+ currentItemActivator.Slot);

        currentItemActivator.Slot.ActionStarted?.Invoke();
    }
}