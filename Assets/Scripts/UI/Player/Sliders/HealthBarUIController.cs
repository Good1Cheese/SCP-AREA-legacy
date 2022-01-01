using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class HealthBarUIController : StatisticsBarUIController
{
    [SerializeField] private AnimationCurve _healthCurve;
    [SerializeField] private float _maxCurveTime;

    [Inject] private readonly PlayerHealth _playerHealth;

    private float _curveTime;
    private Func<bool> _condition;
    private sbyte _deltaTimeMultipliyer;

    private float CurrentHealth => _healthCurve.Evaluate(_curveTime);

    public float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            _slider.value = CurrentHealth;
        }
    }

    private void Awake()
    {
        _curveTime = _maxCurveTime;
    }

    protected override float GetValue()
    {
        return _playerHealth.Amount;
    }

    public override void UpdateUI()
    {
        GetConditionAndDeltaTimeMuitipliyer();
        StartCoroutine(UpdateUICoroutine());
    }

    private void GetConditionAndDeltaTimeMuitipliyer()
    {
        float health = GetValue();
        if (CurrentHealth > health)
        {
            _condition = () => CurrentHealth > health;
            _deltaTimeMultipliyer = -1;
            return;
        }

        _condition = () => CurrentHealth < health;
        _deltaTimeMultipliyer = 1;
    }

    public IEnumerator UpdateUICoroutine()
    {
        while (_condition())
        {
            CurveTime += Time.deltaTime * _deltaTimeMultipliyer;
            yield return null;
        }
    }

    protected override void Subscribe()
    {
        _playerHealth.Changed += UpdateUI;
        _gameLoader.Loaded += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerHealth.Changed -= UpdateUI;
        _gameLoader.Loaded -= UpdateUI;
    }
}