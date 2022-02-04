using UnityEngine;
using Zenject;

[RequireComponent(typeof(WalkSound), typeof(RunSound), typeof(WalkController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private AnimationCurve _movementSpeed;
    [SerializeField] private MoveController[] _moveControllers;

    private WalkController _walkController;
    private MovementController _movementController;
    private MoveController _usingMoveController;

    public AnimationCurve MovementSpeed => _movementSpeed;
    public float SlowDownFactor { get; set; }
    public float MoveTime { get; set; }
    public float Speed { get; set; }
    public float StepTime { get; set; }

    [Inject]
    private void Construct(WalkController walkController, MovementController movementController)
    {
        _walkController = walkController;
        _movementController = movementController;
    }

    public float GetPlayerSpeed()
    {
        GetPlayerMoves();
        _usingMoveController.UpdateFov();

        if (MoveTime > _usingMoveController.MaxMoveTime)
        {
            MoveTime -= Time.deltaTime;
        }

        return Speed - SlowDownFactor;
    }

    private void GetPlayerMoves()
    {
        Speed = 0;

        GetArrayMoves();

        if (Speed == 0)
        {
            _usingMoveController = _walkController;
            Speed = _walkController.GetMove();
        }

        _usingMoveController.InvokeStepInvoke();
    }

    private void GetArrayMoves()
    {
        for (int i = 0; i < _moveControllers.Length; i++)
        {
            float speed = _moveControllers[i].GetSpeed();

            if (speed == 0) { continue; }

            Speed = speed;
            _usingMoveController = _moveControllers[i];
        }
    }
}