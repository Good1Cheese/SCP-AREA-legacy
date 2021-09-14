using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(MovementSpeed), typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{
    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PauseMenu m_pauseMenu;
    [Inject] readonly Transform m_playerTransform;
    CharacterController m_characterController;

    bool IsPlayerMoving;
    public Action OnPlayerStoppedMoving { get; set; }

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_pauseMenu.OnPauseMenuButtonPressed += SetActiveOrUnActive;
    }

    void SetActiveOrUnActive()
    {
        enabled = !enabled;
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove == 0 && verticalMove == 0) 
        { 
            if (IsPlayerMoving)
            {
                OnPlayerStoppedMoving?.Invoke();
            }

            IsPlayerMoving = false;
            return;
        }
        IsPlayerMoving = true;
        float moveSpeed = m_playerSpeed.GetPlayerSpeed();

        Vector3 move = m_playerTransform.right * horizontalMove + m_playerTransform.forward * verticalMove;
        move = Vector3.ClampMagnitude(move, 1f) * Time.deltaTime;
        m_characterController.Move(move * moveSpeed);
    }

    void OnDestroy()
    {
        m_pauseMenu.OnPauseMenuButtonPressed += SetActiveOrUnActive;
    }
}
