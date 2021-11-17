using UnityEngine;

public class GravityForce : MonoBehaviour
{
    [SerializeField] private float _initialVelocityValue;
    [SerializeField] private float _gravity;
    private CharacterController _characterController;
    private Vector3 _velocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
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