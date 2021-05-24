using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerSpeed))]
public class PlayerMovement : MonoBehaviour
{
    PlayerSpeed playerSpeed;
    CharacterController characterController;

    void Start()
    {
        playerSpeed = GetComponent<PlayerSpeed>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        
        if (horizontalMove == 0 && verticalMove == 0) { return; }
        float moveSpeed = playerSpeed.GetPlayerSpeed();

        Vector3 move = transform.right * horizontalMove + transform.forward * verticalMove;
        move = Vector3.ClampMagnitude(move, 1f) * Time.deltaTime;
        characterController.Move(move * moveSpeed);
    }

}
