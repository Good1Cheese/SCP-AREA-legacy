using UnityEngine;
using Zenject;

public class HealTest : IInteractable
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [SerializeField] private int _healthToHeal;

    public override void Interact()
    {
        _playerHealth.Heal(_healthToHeal);
    }
}
