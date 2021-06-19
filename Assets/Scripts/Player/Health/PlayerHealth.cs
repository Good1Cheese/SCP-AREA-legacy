using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterBleeding), typeof(DegreeOfInjuary))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float m_health;
    CharacterBleeding m_bleedingController;
    [Inject] SceneTransition m_sceneTransition;

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
        m_sceneTransition.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}
