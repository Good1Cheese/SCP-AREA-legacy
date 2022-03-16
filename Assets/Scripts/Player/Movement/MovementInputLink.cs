using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(MovesContainer), typeof(PlayerStamina))]
public class MovementInputLink : MonoBehaviour
{
    private const float MOVE_MAGNUTUDE_MAX_LENGHT = 1f;

    private MovesContainer _movesContainer;
    private PauseMenuToggler _pauseMenuToggler;
    private Transform _player;
    private CharacterController _characterController;
    private bool _moving;

    public Action StoppedMoving { get; set; }
    public float HorizontalMove { get; set; }
    public float VerticalMove { get; set; }

    [Inject]
    private void Construct(MovesContainer movesContainer,
                           PauseMenuToggler pauseMenuToggler,
                           [Inject(Id = "Player")] Transform playerTransform,
                           CharacterController characterController)
    {
        _movesContainer = movesContainer;
        _pauseMenuToggler = pauseMenuToggler;
        _player = playerTransform;
        _characterController = characterController;
    }

    private void Awake()
    {
        _pauseMenuToggler.Toggled += ReverseEnableState;
    }

    private void ReverseEnableState() => enabled = !enabled;

    public void Handle(ref Vector2 input)
    {
        HorizontalMove = input.x;
        VerticalMove = input.y;

        if (input.x == 0 && input.y == 0)
        {
            StopMovement();
            DecreasteMoveTime();
            return;
        }

        _moving = true;
        Move(_movesContainer.CheckAndReturnSpeed());
    }

    private void StopMovement()
    {
        if (!_moving) { return; }

        StoppedMoving?.Invoke();
        _moving = false;
    }

    private void DecreasteMoveTime()
    {
        if (_movesContainer.MoveTime < 0) { return; }

        _movesContainer.MoveTime -= Time.deltaTime;
    }

    private void Move(float speed)
    {
        Vector3 move = _player.right * HorizontalMove + _player.forward * VerticalMove;
        move = Vector3.ClampMagnitude(move, MOVE_MAGNUTUDE_MAX_LENGHT) * Time.deltaTime;

        _characterController.Move(move * speed);
    }

    private void OnDestroy()
    {
        _pauseMenuToggler.Toggled -= ReverseEnableState;
    }
}