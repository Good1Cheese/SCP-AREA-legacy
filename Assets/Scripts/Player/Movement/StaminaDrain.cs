using System.Collections;
using UnityEngine;
using Zenject;

public class StaminaDrain : CoroutineUser
{
    [SerializeField] private float _drainPower;
    [SerializeField] private float _recoveryTime;

    private BloodGain _bloodGain;
    private PlayerStamina _playerStamina;

    public float DrainPerSecond { get; set; }
    public float PassedTime { get; set; }

    [Inject]
    private void Construct(PlayerStamina playerStamina, BloodGain bloodGain)
    {
        _bloodGain = bloodGain;
        _playerStamina = playerStamina;
    }

    private new void Start()
    {
        base.Start();
        _bloodGain.GainStarted += StopCoroutine;
        _bloodGain.GainStarted += StartWithoutInterrupt;
    }

    public override void StartWithoutInterrupt()
    {
        base.StartWithoutInterrupt();

        _playerStamina.StartWithoutInterrupt();

        float drainValue = GetDrainValue();
        _playerStamina.MaxCurveTime -= drainValue;
        DrainPerSecond = drainValue / _recoveryTime;
    }

    protected override IEnumerator Coroutine()
    {
        PassedTime = 0;

        while (PassedTime < _recoveryTime)
        {
            PassedTime += Time.deltaTime;
            _playerStamina.MaxCurveTime += Time.deltaTime * DrainPerSecond;

            yield return null;
        }

        _playerStamina.MaxCurveTime = Mathf.Round(_playerStamina.MaxCurveTime);
        IsCoroutineGoing = false;
    }

    private float GetDrainValue()
    {
        return _playerStamina.MaxCurveTime * _drainPower / 100;
    }

    private void OnDestroy()
    {
        _bloodGain.GainStarted -= StopCoroutine;
        _bloodGain.GainStarted -= StartWithoutInterrupt;
    }
}