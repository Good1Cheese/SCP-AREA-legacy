using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(MovementController), typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{
    const float MOVE_MAGNUTUDE_MAX_LENGHT = 1f;

    [Inject] readonly MovementController m_movementController;
    [Inject] readonly WalkController m_walkController;
    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    CharacterController m_characterController;

    bool IsPlayerMoving;
    float m_horizontalMove;
    float m_verticalMove;

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_pauseMenu.OnPauseMenuButtonPressed += ReverseEnableState;
    }

    void ReverseEnableState()
    {
        enabled = !enabled;
    }

    void Update()
    {
        IsPlayerMoving = true;

        m_horizontalMove = Input.GetAxis("Horizontal");
        m_verticalMove = Input.GetAxis("Vertical");

        if (m_horizontalMove == 0 && m_verticalMove == 0)
        {
            if (IsPlayerMoving)
            {
                m_walkController.StopMove();
            }

            IsPlayerMoving = false;
            return;
        }

        MovePlayer(m_movementController.GetPlayerSpeed());
    }

    void MovePlayer(float moveSpeed)
    {
        Vector3 move = m_playerTransform.right * m_horizontalMove + m_playerTransform.forward * m_verticalMove;
        move = Vector3.ClampMagnitude(move, MOVE_MAGNUTUDE_MAX_LENGHT) * Time.deltaTime;
        m_characterController.Move(move * moveSpeed);
    }

    void OnDestroy()
    {
        m_pauseMenu.OnPauseMenuButtonPressed -= ReverseEnableState;
    }
}
