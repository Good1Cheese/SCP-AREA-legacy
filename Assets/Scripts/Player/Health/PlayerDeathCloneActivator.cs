using UnityEngine;
using Zenject;

public class PlayerDeathCloneActivator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject(Id = "Player")] private readonly Transform _playerTransform;

    private DeathAnimationPlayer _playerDeathAnimation;

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