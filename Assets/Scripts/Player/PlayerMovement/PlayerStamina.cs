using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(StaminaUseDisabler))]
public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private int _burnSpeedMultipliyer;
    [SerializeField] private AnimationCurve _staminaCurve;
    [SerializeField] private float _delayBeforeRegenerationStart;
    [SerializeField] private float _staminaValue;
    [SerializeField] private float _staminaTime;

    [Inject] private readonly RunController _runController;
    [Inject] private readonly PlayerMovement _playerMovement;
    private WaitForSeconds _timeoutBeforeRegeneration;
    private IEnumerator _regenerationCoroutine;
    private bool _isRegenerating;
    private float _maxStaminaTime;

    public float Stamina
    {
        get => _staminaValue;
        set
        {
            _staminaValue = value;
            OnStaminaValueChanged?.Invoke();
        }
    }

    public float StaminaTime
    {
        get => _staminaTime;
        set
        {
            _staminaTime = value;
            Stamina = _staminaCurve.Evaluate(_staminaTime);
        }
    }

    public Action OnStaminaValueChanged { get; set; }
    public Action OnStaminaRunningOut { get; set; }
    public bool HasTimeoutPassed { get; set; }

    private void Awake()
    {
        _maxStaminaTime = _staminaTime;
        _regenerationCoroutine = RegenerateCoroutine();
        _timeoutBeforeRegeneration = new WaitForSeconds(_delayBeforeRegenerationStart);
    }

    private void Start()
    {
        _runController.OnPlayerUsingMove += Burn;
        _runController.OnPlayerStartedUseOfMove += StopRegeneration;
        _runController.OnPlayerStoppedUseOfMove += StartRegeneration;
        _playerMovement.OnPlayerStoppedMoving += StartRegeneration;
    }

    private void Burn()
    {
        StaminaTime -= Time.deltaTime * _burnSpeedMultipliyer;
    }

    public void StartRegeneration()
    {
        if (_isRegenerating) { return; }

        StartCoroutine(_regenerationCoroutine);
    }

    public void StopRegeneration()
    {
        _staminaTime = (_staminaTime > _maxStaminaTime) ? _maxStaminaTime : _staminaTime;
        HasTimeoutPassed = false;
        _isRegenerating = false;

        StopCoroutine(_regenerationCoroutine);
        _regenerationCoroutine = RegenerateCoroutine();
    }

    private void Update()
    {
        if (!HasTimeoutPassed) { return; }

        StaminaTime += Time.deltaTime;
    }

    private IEnumerator RegenerateCoroutine()
    {
        _isRegenerating = true;

        yield return _timeoutBeforeRegeneration;
        HasTimeoutPassed = true;
    }

    private void OnDestroy()
    {
        _runController.OnPlayerUsingMove -= Burn;
        _runController.OnPlayerStartedUseOfMove -= StopRegeneration;
        _runController.OnPlayerStoppedUseOfMove -= StartRegeneration;
        _playerMovement.OnPlayerStoppedMoving -= StartRegeneration;
    }
}