using System.Collections;
using UnityEngine;
using Zenject;

public class HealableHealth : CoroutineWithDelayUser
{
    [SerializeField] private float _delayDuringCoroutine;
    [SerializeField] private int _healthBottomBorder;
    [SerializeField] private int _healAmount;

    private PlayerHealth _playerHealth;
    private WaitForSeconds _timeoutDuringCoroutine;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Awake()
    {
        _timeoutDuringCoroutine = new WaitForSeconds(_delayDuringCoroutine);
    }

    private new void Start()
    {
        base.Start();
        _playerHealth.Damaged += StopCoroutine;
        _playerHealth.Damaged += StartWithoutInterrupt;
        _playerHealth.Healed += StartWithoutInterrupt;
    }

    public override void StartWithoutInterrupt()
    {
        if (_playerHealth.Amount < _healthBottomBorder) { return; }

        base.StartWithoutInterrupt();
    }

    protected override IEnumerator Coroutine()
    {
        while (_playerHealth.Amount <= _playerHealth.MaxAmount)
        {
            _playerHealth.Amount += _healAmount;
            _playerHealth.Healed?.Invoke();

            yield return _timeoutDuringCoroutine;
        }

        IsCoroutineGoing = false;
    }

    private void OnDestroy()
    {
        _playerHealth.Damaged -= StopCoroutine;
        _playerHealth.Damaged -= StartWithoutInterrupt;
        _playerHealth.Healed -= StartWithoutInterrupt;
    }
}