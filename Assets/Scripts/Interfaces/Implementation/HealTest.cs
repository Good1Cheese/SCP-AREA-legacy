using UnityEngine;
using Zenject;

public class HealTest : IInteractable
{
    [Inject] PlayerHealth m_playerHealth;

    public override void Interact()
    {
        m_playerHealth.Heal();
    }
}
