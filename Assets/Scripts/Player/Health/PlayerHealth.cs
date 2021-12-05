using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterBleeding), typeof(PlayerDamageSound))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;

    [Inject] private readonly CharacterBleeding _characterBleeding;

    public int Amount { get => _health; set => _health = value; }
    public int MaxAmount => _maxHealth;

    public Action OnPlayerGetsDamage { get; set; }
    public Action OnPlayerHeals { get; set; }
    public Action OnPlayerGetsNonBleedDamage { get; set; }
    public Action OnPlayerDies { get; set; }

    public void Damage(int damage)
    {
        DamageWithOutNotify(damage);
        OnPlayerGetsNonBleedDamage?.Invoke();
    }

    public void DamageWithOutNotify(int damage)
    {
        if (_health - damage <= 0)
        {
            _health = 0;
            Die();
        }

        _health -= damage;
        OnPlayerGetsDamage?.Invoke();
    }

    public void Heal(int healthToHeal)
    {
        if (_characterBleeding.IsBleeding) { return; }

        _health += healthToHeal;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        OnPlayerHeals?.Invoke();
    }

    public void Die()
    {
        OnPlayerDies?.Invoke();
    }
}