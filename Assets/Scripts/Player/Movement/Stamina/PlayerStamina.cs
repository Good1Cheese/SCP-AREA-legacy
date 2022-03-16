using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StaminaDisabler))]
public class PlayerStamina : CoroutineWithDelayUser
{
    [SerializeField] private int _burnSpeedMultipliyer;
    [SerializeField] private AnimationCurve _curve;

    private Run _run;
    private SlowWalkRun _slowWalkRun;
    private MovementInputLink _movementInputLink;

    private float _maxAmount;
    private float _curveTime;
    private float _maxCurveTime;

    public float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = Mathf.Clamp(value, 0, _maxCurveTime);
            Amount = _curve.Evaluate(_curveTime);
            Changed?.Invoke();
        }
    }

    public float MaxCurveTime
    {
        get => _maxCurveTime;
        set
        {
            _maxCurveTime = value;
            CurveTime = CurveTime;
        }
    }

    public int BurnSpeedMultipliyer { get => _burnSpeedMultipliyer; set => _burnSpeedMultipliyer = value; }
    public float Amount { get; set; }
    public Action Changed { get; set; }

    [Inject]
    private void Construct(Run runController,
                           SlowWalkRun slowWalkRun,
                           MovementInputLink playerMovement)
    {
        _run = runController;
        _slowWalkRun = slowWalkRun;
        _movementInputLink = playerMovement;
    }

    private void Awake()
    {
        Keyframe keyframe = _curve.GetLastKeyFrame();

        _maxCurveTime = keyframe.time;
        _curveTime = _maxCurveTime;
        _maxAmount = keyframe.value;
        Amount = _maxAmount;
    }

    private new void Start()
    {
        base.Start();

        _slowWalkRun.Actions.Using += Burn;
        _run.Actions.Using += Burn;
        _slowWalkRun.Actions.UseStarted += StopCoroutine;
        _run.Actions.UseStarted += StopCoroutine;
        _slowWalkRun.Actions.UseStopped += StartWithoutInterrupt;
        _run.Actions.UseStopped += StartWithoutInterrupt;
        _movementInputLink.StoppedMoving += StartWithoutInterrupt;
    }

    private void Burn()
    {
        CurveTime -= Time.deltaTime * _burnSpeedMultipliyer;
    }

    protected override IEnumerator Coroutine()
    {
        while (_curveTime < _maxCurveTime)
        {
            CurveTime += Time.deltaTime;

            yield return null;
        }

        IsCoroutineGoing = false;
    }

    private void OnDestroy()
    {
        _slowWalkRun.Actions.Using -= Burn;
        _run.Actions.Using -= Burn;
        _slowWalkRun.Actions.UseStarted -= StopCoroutine;
        _run.Actions.UseStarted -= StopCoroutine;
        _slowWalkRun.Actions.UseStopped -= StartWithoutInterrupt;
        _run.Actions.UseStopped -= StartWithoutInterrupt;
        _movementInputLink.StoppedMoving -= StartWithoutInterrupt;
    }
}