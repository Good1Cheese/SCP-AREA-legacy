using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(MovementSpeed), typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{
    [Inject] readonly MovementSpeed m_playerSpeed;
    CharacterController m_characterController;
    Transform m_transform;

    public bool IsPlayerMoving { get; set; }

    void Start()
    {
        m_transform = transform;
        m_characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        IsPlayerMoving = false;
        if (horizontalMove == 0 && verticalMove == 0) { return; }
        IsPlayerMoving = true;
        float moveSpeed = m_playerSpeed.GetPlayerSpeed();
 
        Vector3 move = m_transform.right * horizontalMove + m_transform.forward * verticalMove;
        move = Vector3.ClampMagnitude(move, 1f) * Time.deltaTime;
        m_characterController.Move(move * moveSpeed);
    }

}
