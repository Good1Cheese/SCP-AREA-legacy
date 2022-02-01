using UnityEngine;
using Zenject;

public class DeathTest : MonoBehaviour, IInteractable
{
    private PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public void Interact()
    {
        _playerHealth.Damage(_playerHealth.Amount);
    }
}