using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerSpeed))]
public class PlayerMovement : MonoBehaviour
{
    PlayerSpeed _playerSpeed;
    CharacterController _characterController;
    Transform _transform;

    void Start()
    {
        _transform = transform;
        _playerSpeed = GetComponent<PlayerSpeed>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove == 0 && verticalMove == 0) { return; }
        float moveSpeed = _playerSpeed.GetPlayerSpeed();

        Vector3 move = _transform.right * horizontalMove + _transform.forward * verticalMove;
        move = Vector3.ClampMagnitude(move, 1f) * Time.deltaTime;
        _characterController.Move(move * moveSpeed);
    }

}
