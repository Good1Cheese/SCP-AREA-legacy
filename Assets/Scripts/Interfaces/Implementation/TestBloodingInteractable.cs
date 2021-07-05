using UnityEngine;
using Zenject;

public class TestBloodingInteractable : MonoBehaviour, IInteractable
{
    [Inject] readonly CharacterBleeding m_playerBleeding;
    public void Interact()
    {
        m_playerBleeding.Bleed();
    }
}
