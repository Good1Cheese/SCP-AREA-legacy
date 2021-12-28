using System.Collections;
using UnityEngine;
using Zenject;

public class StaminaDrain : CoroutineUser
{
    [SerializeField] private float _drainPower;
    [SerializeField] private float _recoveryTime;

    [Inject] readonly private BloodGain _bloodGain;
    [Inject] readonly private PlayerStamina _playerStamina;

    private new void Start()
    {
        base.Start();
        _bloodGain.GainStarted += StartAction;
    }

    protected override void StartAction()
    {
        base.StartAction();
        _playerStamina.MaxCurveTime -= 
    }

    protected override IEnumerator Coroutine()
    {
        yield break;
    }

    private void OnDestroy()
    {
        _bloodGain.GainStarted -= StartAction;
    }
}