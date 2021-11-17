using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _minDelay;

    [Inject] private readonly PlayerHealth _playerHealth;
    private WaitForSeconds _timeoutDuringBleeding;
    private IEnumerator _bleedCoroutine;

    public bool IsBleeding { get; set; }
    public Action OnPlayerBleeding { get; set; }

    private void Start()
    {
        _bleedCoroutine = BleedCoroutine();
        SetBleedingDelay();
    }

    public void Bleed()
    {
        if (IsBleeding) { return; }

        IsBleeding = true;
        StartCoroutine(_bleedCoroutine);
    }

    public void StopBleeding()
    {
        StopBleedingWithoutNotify();
    }

    public void StopBleedingWithoutNotify()
    {
        StopCoroutine(_bleedCoroutine);
        _bleedCoroutine = BleedCoroutine();

        IsBleeding = false;
    }

    public void SetBleedingDelay()
    {
        float delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
        _timeoutDuringBleeding = new WaitForSeconds(delay);
    }

    private IEnumerator BleedCoroutine()
    {
        while (_playerHealth.Amount > 0)
        {
            yield return _timeoutDuringBleeding;

            _playerHealth.DamageWithOutNotify(_damage);
            OnPlayerBleeding?.Invoke();
            SetBleedingDelay();
        }

        IsBleeding = false;
    }
}
