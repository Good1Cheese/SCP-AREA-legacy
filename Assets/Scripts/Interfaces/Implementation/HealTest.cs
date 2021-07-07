using UnityEngine;
using Zenject;

public class HealTest : IInteractable
{
    [Inject] CharacterBleeding m_playerHealth;

    public override void Interact()
    {
        m_playerHealth.StopBleeding();
    }
}
