using System;
using Zenject;
using UnityEngine;

[RequireComponent(typeof(CharacterBleeding), typeof(PlayerDamageSound))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int m_maxHealth;
    [SerializeField] int m_health;

    [Inject] readonly CharacterBleeding m_characterBleeding;

    public int Amount { get => m_health; set => m_health = value; }
    public int MaxAmount { get => m_maxHealth; }

    public Action OnPlayerDies { get; set; }
    public Action OnPlayerGetsDamage { get; set; }
    public Action OnPlayerGetsNonBleedDamage { get; set; }
    public Action OnPlayerHeals { get; set; }

    public void Damage(int damage)
    {
        DamageWithOutNotify(damage);
        OnPlayerGetsNonBleedDamage?.Invoke();
    }

    public void DamageWithOutNotify(int damage)
    {
        if (m_health - damage <= 0)
        {
            m_health = 0;
            Die();
        }

        m_health -= damage;
        OnPlayerGetsDamage?.Invoke();
    }

    public int GetHealthPercent()
    {
        return m_health / 25;
    }

    public void Heal(int healthToHeal)
    {
        if (m_characterBleeding.IsBleeding) { return; }

        m_health += healthToHeal;
        m_health = Mathf.Clamp(m_health, 0, m_maxHealth);

        OnPlayerHeals?.Invoke();
    }

    public void Die()
    {
        OnPlayerDies?.Invoke();
        //Destroy(gameObject);
    }

}