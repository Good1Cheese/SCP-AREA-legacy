using UnityEngine;
using Zenject;
using System;

public class HealthBarUpdater : MonoBehaviour
{
    [SerializeField] private RiseableCurve _healthCurve;

    private PlayerHealth _playerHealth;

    public HealthBarUIController HealthBarUIController { get; set; }

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Awake()
    {
        Func<float, bool> decreaseCondition = curveValue => curveValue > _playerHealth.Amount;
        Func<float, bool> riseCondition = curveValue => curveValue < _playerHealth.Amount;

        _healthCurve.Initialize(riseCondition, decreaseCondition);
        _healthCurve.CurveTime = _healthCurve.Curve.GetLastKeyFrame().time;
    }

    private void Start()
    {
        _healthCurve.Changed += UpdateHealthAmount;
    }

    private void UpdateHealthAmount()
    {
        HealthBarUIController.Slider.value = _healthCurve.Evaluate();
    }

    public void UpdateUI()
    {
        if (_healthCurve.Evaluate() > _playerHealth.Amount)
        {
            _healthCurve.Decrease();
            return;
        }

        _healthCurve.Rise();
    }

    private void OnDestroy()
    {
        _healthCurve.Changed -= UpdateHealthAmount;
    }
}