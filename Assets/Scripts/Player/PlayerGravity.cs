using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] float _initialVelocityValue;
    [SerializeField] float _gravity;
    CharacterController _characterController;
    Vector3 _velocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = _initialVelocityValue;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}