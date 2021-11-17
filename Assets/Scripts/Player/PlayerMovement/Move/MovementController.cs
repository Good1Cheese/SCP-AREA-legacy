using UnityEngine;
using Zenject;

[RequireComponent(typeof(WalkSound), typeof(RunSound), typeof(WalkController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private AnimationCurve _movementSpeed;
    [SerializeField] private MoveController[] _moveControllers;

    [Inject] private readonly WalkController _walkController;
    private MoveController _usingMoveController;

    public AnimationCurve MovementSpeed => _movementSpeed;
    public float SlowDownFactor { get; set; }
    public float MoveTime { get; set; }
    public float Speed { get; set; }

    public float GetPlayerSpeed()
    {
        GetSpeedOfMoveControllers();

        if (MoveTime > _usingMoveController.MaxMoveTime)
        {
            MoveTime -= Time.deltaTime;
        }

        return Speed - SlowDownFactor;
    }

    private void GetSpeedOfMoveControllers()
    {
        Speed = 0;

        foreach (MoveController controller in _moveControllers)
        {
            if (Speed != 0) { break; }

            controller.StopMove();
            Speed = controller.GetMove();

            _usingMoveController = controller;
        }

        if (Speed == 0)
        {
            _usingMoveController = _walkController;
            Speed = _walkController.GetMove();
        }
    }
}
