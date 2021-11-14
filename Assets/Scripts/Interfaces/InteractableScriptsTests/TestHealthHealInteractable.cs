using UnityEngine;
using Zenject;

public class TestHealthHealInteractable : IInteractable
{
    [SerializeField] int m_damage;

    [Inject] readonly PlayerHealth m_playerBleeding;

    public override void Interact()
    {
        m_playerBleeding.Damage(m_damage);
    }
}
