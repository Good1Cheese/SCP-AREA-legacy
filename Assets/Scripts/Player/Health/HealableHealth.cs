using System.Collections;
using UnityEngine;
using Zenject;

public class HealableHealth : MonoBehaviour
{
    [SerializeField] private int _healthBottomBorder;
    [SerializeField] private float _healDelay;
    [SerializeField] private float _delayBeforeHeal;
    [SerializeField] private int _healAmount;

    [Inject] private readonly PlayerHealth _playerHealth;
    private WaitForSeconds _healTimeout;
    private WaitForSeconds _timeoutBeforeHeal;
    private IEnumerator _healEnumerator;

    public bool IsHealGoing { get; set; }

    private void Awake()
    {
        _healEnumerator = HealCoroutine();
        _healTimeout = new WaitForSeconds(_healDelay);
        _timeoutBeforeHeal = new WaitForSeconds(_delayBeforeHeal);
    }

    private void Start()
    {
        _playerHealth.OnPlayerGetsDamage += StopHeal;
        _playerHealth.OnPlayerGetsDamage += Heal;
        _playerHealth.OnPlayerHeals += Heal;
    }

    private void Heal()
    {
        if (IsHealGoing) { return; }

        if (_playerHealth.Amount >= _healthBottomBorder)
        {
            StartHeal();
        }
    }

    public void StartHeal()
    {
        IsHealGoing = true;
        _healEnumerator = HealCoroutine();
        StartCoroutine(_healEnumerator);
    }

    private void StopHeal()
    {
        IsHealGoing = false;
        StopCoroutine(_healEnumerator);
        _healEnumerator = HealCoroutine();
    }

    private IEnumerator HealCoroutine()
    {
        yield return _timeoutBeforeHeal;

        while (_playerHealth.Amount <= _playerHealth.MaxAmount)
        {
            _playerHealth.Amount += _healAmount;
            _playerHealth.OnPlayerHeals?.Invoke();

            yield return _healTimeout;
        }
        IsHealGoing = false;
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerGetsDamage -= StopHeal;
        _playerHealth.OnPlayerGetsDamage -= Heal;
        _playerHealth.OnPlayerHeals -= Heal;
    }
}
