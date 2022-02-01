using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StaminaDisabler))]
public class PlayerStamina : CoroutineWithDelayUser
{
    [SerializeField] private int _burnSpeedMultipliyer;
    [SerializeField] private AnimationCurve _curve;

    private RunController _runController;
    private SlowWalkRunController _slowWalkRunController;
    private PlayerMovement _playerMovement;

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
    private void Construct(RunController runController,
                           SlowWalkRunController slowWalkRunController,
                           PlayerMovement playerMovement)
    {
        _runController = runController;
        _slowWalkRunController = slowWalkRunController;
        _playerMovement = playerMovement;
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
        _slowWalkRunController.Using += Burn;
        _runController.Using += Burn;
        _slowWalkRunController.UseStarted += StopCoroutine;
        _runController.UseStarted += StopCoroutine;
        _slowWalkRunController.UseStopped += StartWithoutInterrupt;
        _runController.UseStopped += StartWithoutInterrupt;
        _playerMovement.StoppedMoving += StartWithoutInterrupt;
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
        _slowWalkRunController.Using -= Burn;
        _runController.Using -= Burn;
        _slowWalkRunController.UseStarted -= StopCoroutine;
        _runController.UseStarted -= StopCoroutine;
        _slowWalkRunController.UseStopped -= StartWithoutInterrupt;
        _runController.UseStopped -= StartWithoutInterrupt;
        _playerMovement.StoppedMoving -= StartWithoutInterrupt;
    }
}