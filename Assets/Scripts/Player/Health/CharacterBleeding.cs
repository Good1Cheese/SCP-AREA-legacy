using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : CoroutineUser
{
    [SerializeField] private int _damage;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _minDelay;

    [Inject] private readonly PlayerHealth _playerHealth;

    public Action OnPlayerBleeding { get; set; }

    private void Awake()
    {
        SetBleedingDelay();
    }

    public void SetBleedingDelay()
    {
        float delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
        _coroutineTimeout = new WaitForSeconds(delay);
    }

    protected override IEnumerator Coroutine()
    {
        while (_playerHealth.Amount > 0)
        {
            yield return _coroutineTimeout;

            _playerHealth.DamageWithOutNotify(_damage);
            OnPlayerBleeding?.Invoke();
            SetBleedingDelay();
        }

        IsActionGoing = false;
    }
}