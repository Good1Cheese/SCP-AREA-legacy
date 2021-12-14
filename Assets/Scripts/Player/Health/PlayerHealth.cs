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

    public Action Damaged { get; set; }
    public Action Healed { get; set; }
    public Action GetsNonBleedDamage { get; set; }
    public Action Died { get; set; }

    public void Damage(int damage)
    {
        DamageWithOutNotify(damage);
        GetsNonBleedDamage?.Invoke();
    }

    public void DamageWithOutNotify(int damage)
    {
        if (_health - damage <= 0)
        {
            _health = 0;
            Die();
        }

        _health -= damage;
        Damaged?.Invoke();
    }

    public void Heal(int healthToHeal)
    {
        if (_characterBleeding.IsActionGoing) { return; }

        _health += healthToHeal;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        Healed?.Invoke();
    }

    public void Die()
    {
        Died?.Invoke();
    }
}