using System.Collections;
using UnityEngine;
using Zenject;

public class HealableHealth : CoroutineUser
{
    [SerializeField] private int _healthBottomBorder;
    [SerializeField] private float _delayBeforeHeal;
    [SerializeField] private int _healAmount;

    [Inject] private readonly PlayerHealth _playerHealth;
    private WaitForSeconds _timeoutBeforeHeal;

    private void Awake()
    {
        _playerHealth.OnPlayerGetsDamage += StopAction;
        _playerHealth.OnPlayerGetsDamage += Heal;
        _playerHealth.OnPlayerHeals += Heal;
    }

    private new void Start()
    {
        base.Start();
        _timeoutBeforeHeal = new WaitForSeconds(_delayBeforeHeal);
    }

    private void Heal()
    {
        if (_playerHealth.Amount >= _healthBottomBorder)
        {
            StartAction();
        }
    }

    protected override IEnumerator Coroutine()
    {
        yield return _timeoutBeforeHeal;

        while (_playerHealth.Amount <= _playerHealth.MaxAmount)
        {
            _playerHealth.Amount += _healAmount;
            _playerHealth.OnPlayerHeals?.Invoke();

            yield return _coroutineTimeout;
        }

        IsActionGoing = false;
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerGetsDamage -= StopAction;
        _playerHealth.OnPlayerGetsDamage -= Heal;
        _playerHealth.OnPlayerHeals -= Heal;
    }
}