using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StaminaDisabler))]
public class PlayerStamina : CoroutineUser
{
    [SerializeField] private AnimationCurve _staminaCurve;
    [SerializeField] private float _stamina;
    [SerializeField] private float _staminaTime;
    [SerializeField] private int _burnSpeedMultipliyer;

    [Inject] private readonly RunController _runController;
    [Inject] private readonly PlayerMovement _playerMovement;

    public float StaminaTime
    {
        get => _staminaTime;
        set
        {
            _staminaTime = value;
            Stamina = _staminaCurve.Evaluate(_staminaTime);
            OnStaminaValueChanged?.Invoke();
        }
    }

    public float Stamina { get => _stamina; set => _stamina = value; }
    public int BurnSpeedMultipliyer { get => _burnSpeedMultipliyer; set => _burnSpeedMultipliyer = value; }
    public float MaxStaminaTime { get; private set; }
    public bool IsTimeoutPassed { get; set; }
    public Action OnStaminaValueChanged { get; set; }
    public Action OnStaminaRunningOut { get; set; }

    private void Awake()
    {
        _runController.OnPlayerUsingMove += Burn;
        _runController.OnPlayerStartedUsing += StopRegeneration;
        _runController.OnPlayerStoppedUsing += StartAction;
        _playerMovement.OnPlayerStoppedMoving += StartAction;
    }

    private new void Start()
    {
        base.Start();

        MaxStaminaTime = _staminaTime;
    }

    private void Update()
    {
        if (!IsTimeoutPassed) { return; }

        StaminaTime += Time.deltaTime;
    }

    private void Burn()
    {
        StaminaTime -= Time.deltaTime * _burnSpeedMultipliyer;
    }

    public void StopRegeneration()
    {
        _staminaTime = (_staminaTime > MaxStaminaTime) ? MaxStaminaTime : _staminaTime;
        IsTimeoutPassed = false;

        StopAction();
    }

    protected override IEnumerator Coroutine()
    {
        IsActionGoing = true;

        yield return _coroutineTimeout;

        IsTimeoutPassed = true;
    }

    private void OnDestroy()
    {
        _runController.OnPlayerUsingMove -= Burn;
        _runController.OnPlayerStartedUsing -= StopRegeneration;
        _runController.OnPlayerStoppedUsing -= StartAction;
        _playerMovement.OnPlayerStoppedMoving -= StartAction;
    }
}