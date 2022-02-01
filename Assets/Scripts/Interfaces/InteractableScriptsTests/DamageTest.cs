using UnityEngine;
using Zenject;

public class DamageTest : MonoBehaviour, IInteractable
{
    [SerializeField] private int _damage;

    private PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public void Interact()
    {
        _playerHealth.Damage(_damage);
    }
}