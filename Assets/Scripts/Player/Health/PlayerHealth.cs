using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerBlood), typeof(PlayerDamageSound))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _amount;

    [Inject] private readonly PlayerBlood _playerBlood;

    public int Amount { get => _amount; set => _amount = value; }
    public int MaxAmount => _maxHealth;

    public Action Damaged { get; set; }
    public Action Healed { get; set; }
    public Action GotNonBleedDamage { get; set; }
    public Action Died { get; set; }

    public void Damage(int damage)
    {
        if (_amount - damage <= 0)
        {
            _amount = 0;
            Die();
        }

        _amount -= damage;
        Damaged?.Invoke();

        GotNonBleedDamage?.Invoke();
    }

    public void Heal(int healthToHeal)
    {
        if (_playerBlood.IsActionGoing) { return; }

        _amount += healthToHeal;
        _amount = Mathf.Clamp(_amount, 0, _maxHealth);

        Healed?.Invoke();
    }

    public void Die()
    {
        Died?.Invoke();
    }
}