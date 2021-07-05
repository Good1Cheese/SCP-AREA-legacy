using UnityEngine;
using Zenject;

public class TestHealthHealInteractable : MonoBehaviour, IInteractable
{
    [Inject] readonly PlayerHealth m_playerBleeding;

    public void Interact()
    {
        m_playerBleeding.Damage();
    }
}
