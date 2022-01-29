using UnityEngine;
using Zenject;

public class HealthBarUpdater : CoroutineInsteadUpdateUser
{
    [SerializeField] private AnimationCurve _curve;

    private PlayerHealth _playerHealth;
    private float _health;

    public override float CurveTime
    {
        get => _curveTime;
        set
        {
            _curveTime = value;
            HealthBarUIController.Slider.value = CurrentHealth;
        }
    }
    public HealthBarUIController HealthBarUIController { get; set; }
    private float CurrentHealth => _curve.Evaluate(_curveTime);

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Awake()
    {
        _curveTime = _curve.GetLastKeyFrame().time;
    }

    public override void InvokeCoroutine()
    {
        _health = _playerHealth.Amount;
        base.InvokeCoroutine();
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