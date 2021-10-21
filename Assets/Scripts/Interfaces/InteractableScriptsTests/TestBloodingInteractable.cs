using UnityEngine;
using Zenject;

public class TestBloodingInteractable : IInteractable
{
    [Inject] readonly CharacterBleeding m_playerBleeding;
    public override void Interact()
    {
        m_playerBleeding.Bleed();
    }
}
