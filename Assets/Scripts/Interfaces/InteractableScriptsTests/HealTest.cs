using UnityEngine;
using Zenject;

public class HealTest : MonoBehaviour, IInteractable
{
    [SerializeField] private int _healthToHeal;

    private PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public void Interact()
    {
        _playerHealth.Heal(_healthToHeal);
    }
}