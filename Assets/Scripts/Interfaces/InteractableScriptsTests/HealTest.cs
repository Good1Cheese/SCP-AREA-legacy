using UnityEngine;
using Zenject;

public class HealTest : IInteractable
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [SerializeField] int m_healthToHeal;

    public override void Interact()
    {
        m_playerHealth.Heal(m_healthToHeal);
    }
}
