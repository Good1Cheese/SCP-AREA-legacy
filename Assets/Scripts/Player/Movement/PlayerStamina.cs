using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StaminaDisabler))]
public class PlayerStamina : CoroutineUser
{
    [SerializeField] private float _coroutineDelay;
    [SerializeField] private float _maxAmount;
    [SerializeField] private int _burnSpeedMultipliyer;
    [SerializeField] private float _curveTime;
    [SerializeField] private float _maxCurveTime;

    [SerializeField] private AnimationCurve _staminaCurve;

    [Inject] private readonly RunController _runController;
    [Inject] private readonly SlowWalkRunController _slowWalkRunController;
    [Inject] private readonly PlayerMovement _playerMovement;

    public float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = Mathf.Clamp(value, 0, _maxCurveTime);
            Amount = _staminaCurve.Evaluate(_curveTime);
            Changed?.Invoke();
        }
    }

    public float MaxCurveTime { get => _maxCurveTime; set => _maxCurveTime = value; }
    public int BurnSpeedMultipliyer { get => _burnSpeedMultipliyer; set => _burnSpeedMultipliyer = value; }
    public float Amount { get; set; }
    public bool IsTimeoutPassed { get; set; }
    public Action Changed { get; set; }

    private void Awake()
    {
        Amount = _maxAmount;
        _coroutineTimeout = new WaitForSeconds(_coroutineDelay);
    }

    private new void Start()
    {
        base.Start();
        _slowWalkRunController.Using += Burn;
        _slowWalkRunController.UseStarted += StopAction;
        _slowWalkRunController.UseStopped += StartActionWithInterrupt;

        _runController.Using += Burn;
        _runController.UseStarted += StopAction;
        _runController.UseStopped += StartActionWithInterrupt;

        _playerMovement.StoppedMoving += StartActionWithInterrupt;
    }

    private void Burn()
    {
        CurveTime -= Time.deltaTime * _burnSpeedMultipliyer;
    }

    public override void StopAction()
    {
        _curveTime = (_curveTime > _maxCurveTime) ? _maxCurveTime : _curveTime;
        IsTimeoutPassed = false;

        base.StopAction();
    }

    protected override IEnumerator Coroutine()
    {
        yield return _coroutineTimeout;

        IsTimeoutPassed = true;
        while (_curveTime < _maxCurveTime)
        {
            CurveTime += Time.deltaTime;

            yield return null;
        }
    }

    private void OnDestroy()
    {
        _slowWalkRunController.Using -= Burn;
        _slowWalkRunController.UseStarted -= StopAction;
        _slowWalkRunController.UseStopped -= StartActionWithInterrupt;

        _runController.Using -= Burn;
        _runController.UseStarted -= StopAction;
        _runController.UseStopped -= StartActionWithInterrupt;

        _playerMovement.StoppedMoving -= StartActionWithInterrupt;
    }
}