using System.Collections;
using UnityEngine;
using Zenject;

public class HealableHealth : CoroutineUser
{
    [SerializeField] private float _coroutineDelay;
    [SerializeField] private int _healthBottomBorder;
    [SerializeField] private float _delayBeforeHeal;
    [SerializeField] private int _healAmount;

    [Inject] private readonly PlayerHealth _playerHealth;
    private WaitForSeconds _timeoutBeforeHeal;

    private void Awake()
    {
        _playerHealth.Damaged += StopAction;
        _playerHealth.Damaged += Heal;
        _playerHealth.Healed += Heal;
    }

    private new void Start()
    {
        base.Start();
        _coroutineTimeout = new WaitForSeconds(_coroutineDelay);
        _timeoutBeforeHeal = new WaitForSeconds(_delayBeforeHeal);
    }

    private void Heal()
    {
        if (_playerHealth.Amount >= _healthBottomBorder)
        {
            StartActionWithInterrupt();
        }
    }

    protected override IEnumerator Coroutine()
    {
        yield return _timeoutBeforeHeal;

        while (_playerHealth.Amount <= _playerHealth.MaxAmount)
        {
            _playerHealth.Amount += _healAmount;
            _playerHealth.Healed?.Invoke();

            yield return _coroutineTimeout;
        }

        IsActionGoing = false;
    }

    private void OnDestroy()
    {
        _playerHealth.Damaged -= StopAction;
        _playerHealth.Damaged -= Heal;
        _playerHealth.Healed -= Heal;
    }
}