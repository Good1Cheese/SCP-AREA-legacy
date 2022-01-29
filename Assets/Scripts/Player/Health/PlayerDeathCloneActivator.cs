using UnityEngine;
using Zenject;

public class PlayerDeathCloneActivator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private PlayerHealth _playerHealth;
    private Transform _playerTransform;
    private DeathAnimationPlayer _playerDeathAnimation;

    [Inject]
    private void Construct(PlayerHealth playerHealth, [Inject(Id = "Player")] Transform playerTransform)
    {
        _playerHealth = playerHealth;
        _playerTransform = playerTransform;
    }

    private void Start()
    {
        _playerDeathAnimation = GetComponent<DeathAnimationPlayer>();
        _playerHealth.Died += ActivateDeathAnimation;
        _gameObject.SetActive(false);
    }

    private void ActivateDeathAnimation()
    {
        _gameObject.transform.SetPositionAndRotation(_playerTransform.position, _playerTransform.rotation);
        _gameObject.SetActive(true);

        _playerDeathAnimation.PlayDeathAnimation();
    }

    private void OnDestroy()
    {
        _playerHealth.Died -= ActivateDeathAnimation;
    }
}