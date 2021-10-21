using UnityEngine;
using Zenject;

public class TestHealthHealInteractable : IInteractable
{
    [Inject] readonly PlayerHealth m_playerBleeding;

    public override void Interact()
    {
        m_playerBleeding.Damage();
    }
}
