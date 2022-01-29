using UnityEngine;
using Zenject;

public class GravityForce : MonoBehaviour
{
    [SerializeField] private float _initialVelocityValue;
    [SerializeField] private float _gravity;

    private CharacterController _characterController;
    private Vector3 _velocity;

    [Inject]
    private void Construct(CharacterController characterController)
    {
        _characterController = characterController;
    }

    private void FixedUpdate()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = _initialVelocityValue;
        }

        _velocity.y += _gravity * Time.fixedDeltaTime;
        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }
}