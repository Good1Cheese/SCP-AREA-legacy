using UnityEngine;
using Zenject;

public class DamageTest : Interactable
{
    [SerializeField] private int _damage;

    private PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public override void Interact()
    {
        _playerHealth.Damage(_damage);
    }
}