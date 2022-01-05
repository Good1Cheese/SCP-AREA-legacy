using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerBlood), typeof(PlayerDamageSound))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private AnimationCurve _bloodDamageMultipliyer;

    [Inject] private readonly PlayerBlood _playerBlood;

    private float _amount;

    public float Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            Changed?.Invoke();
        }
    }
    public float MaxAmount => _maxHealth;

    public Action Damaged { get; set; }
    public Action Changed { get; set; }
    public Action Healed { get; set; }
    public Action Died { get; set; }
    public float CurrentPercentage => (MaxAmount - Amount) / 100;

    private void Awake()
    {
        _amount = MaxAmount;
    }

    public void Damage(float damage)
    {
        damage += GetBloodDamageMultipliyer(damage);
        if (_amount - damage <= 0)
        {
            Amount = 0;
            Die();
        }

        Amount -= damage;
        Damaged?.Invoke();
    }

    public void Heal(float healthToHeal)
    {
        if (_playerBlood.IsCoroutineGoing) { return; }

        _amount += healthToHeal;
        Amount = Mathf.Clamp(_amount, 0, _maxHealth);

        Healed?.Invoke();
    }

    public void Die()
    {
        Died?.Invoke();
    }

    private float GetBloodDamageMultipliyer(float damage)
    {
        return damage * _bloodDamageMultipliyer.Evaluate(_playerBlood.Amount) / 100;
    }
}