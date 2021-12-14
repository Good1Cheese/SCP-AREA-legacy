using UnityEngine;
using Zenject;

public class PlayerDeathCloneActivator : MonoBehaviour
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject(Id = "Player")] private readonly Transform _playerTransform;
    private DeathAnimationPlayer _playerDeathAnimation;
    private GameObject _gameObject;

    private void Awake()
    {
        _gameObject = transform.parent.gameObject;
        _playerDeathAnimation = GetComponent<DeathAnimationPlayer>();
    }

    private void Start()
    {
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
