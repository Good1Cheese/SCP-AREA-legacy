using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(MovementController), typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{
    private const float MOVE_MAGNUTUDE_MAX_LENGHT = 1f;

    private MovementController _movementController;
    private WalkController _walkController;
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private Transform _playerTransform;
    private CharacterController _characterController;
    private bool _isPlayerMoving;

    public Action NotMoving { get; set; }
    public Action StoppedMoving { get; set; }
    public float HorizontalMove { get; set; }
    public float VerticalMove { get; set; }

    [Inject]
    private void Construct(MovementController movementController,
                           WalkController walkController,
                           PauseMenuEnablerDisabler pauseMenuEnablerDisabler,
                           [Inject(Id = "Player")] Transform playerTransform,
                           CharacterController characterController)
    {
        _movementController = movementController;
        _walkController = walkController;
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
        _playerTransform = playerTransform;
        _characterController = characterController;
    }

    private void Awake()
    {
        _pauseMenuEnablerDisabler.EnabledDisabled += ReverseEnableState;
    }

    private void ReverseEnableState()
    {
        enabled = !enabled;
    }

    private void Update()
    {
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        if (HorizontalMove == 0 && VerticalMove == 0)
        {
            NotMoving?.Invoke();
            _movementController.MoveTime = 0;
            _movementController.ÑurrentStepTime = 0;

            if (_isPlayerMoving)
            {
                StoppedMoving?.Invoke();
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
        Vector3 move = _playerTransform.right * HorizontalMove + _playerTransform.forward * VerticalMove;
        move = Vector3.ClampMagnitude(move, MOVE_MAGNUTUDE_MAX_LENGHT) * Time.deltaTime;
        _characterController.Move(move * moveSpeed);
    }

    private void OnDestroy()
    {
        _pauseMenuEnablerDisabler.EnabledDisabled -= ReverseEnableState;
    }
}