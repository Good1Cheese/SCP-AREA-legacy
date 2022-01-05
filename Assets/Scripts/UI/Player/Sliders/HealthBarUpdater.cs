using UnityEngine;
using Zenject;

public class HealthBarUpdater : CoroutineInsteadUpdateUser
{
    [SerializeField] private AnimationCurve _curve;

    [Inject] private readonly PlayerHealth _playerHealth;

    private float _health;

    public HealthBarUIController HealthBarUIController { get; set; }

    private void Awake()
    {
        _curveTime = _curve.GetLastKeyFrame().time;
    }

    private float CurrentHealth => _curve.Evaluate(_curveTime);

    public override float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            HealthBarUIController.Slider.value = CurrentHealth;
        }
    }

    public override void UpdateCoroutine()
    {
        _health = _playerHealth.Amount;
        base.UpdateCoroutine();
    }

    protected override void GetConditionAndDeltaTimeMuitipliyer()
    {
        if (CurrentHealth > _health)
        {
            DecreaseCurveTime();
            _condition = () => CurrentHealth > _health;
            return;
        }

        IncreaseCurveTime();
        _condition = () => CurrentHealth < _health;
    }
}