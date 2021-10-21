using UnityEngine;
using Zenject;

public class TestDeath : IInteractable
{
    [Inject] readonly PlayerHealth m_playerHealth;

    public override void Interact()
    {
        m_playerHealth.Die();
    }
}    
