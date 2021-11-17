using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(MovementController), typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{
    private const float MOVE_MAGNUTUDE_MAX_LENGHT = 1f;

    [Inject] private readonly MovementController _movementController;
    [Inject] private readonly WalkController _walkController;
    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenu;
    [Inject(Id = "Player")] private readonly Transform _playerTransform;
    private CharacterController _characterController;
    private bool _isPlayerMoving;
    private float _horizontalMove;
    private float _verticalMove;

    public Action OnPlayerNotMoving { get; set; }
    public Action OnPlayerStoppedMoving { get; set; }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _pauseMenu.OnPauseMenuButtonPressed += ReverseEnableState;
    }

    private void ReverseEnableState()
    {
        enabled = !enabled;
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");

        if (_horizontalMove == 0 && _verticalMove == 0)
        {
            OnPlayerNotMoving.Invoke();
            _movementController.MoveTime = 0;

            if (_isPlayerMoving)
            {
                OnPlayerStoppedMoving?.Invoke();
                _walkController.StopMove();
                _isPlayerMoving = false;
            }

            return;
        }

        _isPlayerMoving = true;
        MovePlayer(_movementController.GetPlayerSpeed());
    }

    private void MovePlayer(float moveSpeed)
    {
        Vector3 move = _playerTransform.right * _horizontalMove + _playerTransform.forward * _verticalMove;
        move = Vector3.ClampMagnitude(move, MOVE_MAGNUTUDE_MAX_LENGHT) * Time.deltaTime;
        _characterController.Move(move * moveSpeed);
    }

    private void OnDestroy()
    {
        _pauseMenu.OnPauseMenuButtonPressed -= ReverseEnableState;
    }
}
