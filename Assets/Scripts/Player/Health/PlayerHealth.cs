using System;
using UnityEngine;

[RequireComponent(typeof(CharacterBleeding), typeof(DegreeOfInjuary))]
public class PlayerHealth : MonoBehaviour
{
    CharacterBleeding m_bleedingController;

    [SerializeField] float m_health;
    public float Health
    {
        get => m_health;
        set
        {
            m_health = value;
            OnHealthValueChanged?.Invoke();
        }
    }

    public Action OnPlayerGetsDamage { get; set; }
    public Action OnHealthValueChanged { get; set; }

    void Awake()
    {
        m_bleedingController = GetComponent<CharacterBleeding>();
        MainLinks.Instance.PlayerHealth = this;
    }

    public void Damage(float amoutOfDamage)
    {
        Health -= amoutOfDamage;
        if (Health > 0)
        {
            OnPlayerGetsDamage?.Invoke();
            return;
        }
        Die();
    }

    public void DamageBleeding(float amoutOfDamage)
    {
        Health -= amoutOfDamage;
        if (Health > 0)
        {
            return;
        }
        Die();
    }

    public void Scratch(float amoutOfDamage)
    {
        Damage(amoutOfDamage);
        m_bleedingController.Bleed();
    }

    void Die()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}
