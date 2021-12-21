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
        GetMoves();

        if (MoveTime > _usingMoveController.MaxMoveTime)
        {
            MoveTime -= Time.deltaTime;
        }

        return Speed - SlowDownFactor;
    }

    private void GetMoves()
    {
        Speed = 0;

        for (int i = 0; i < _moveControllers.Length; i++)
        {
            float speed = _moveControllers[i].GetSpeed();

            if (speed == 0) { continue; }

            Speed = speed;
            _usingMoveController = _moveControllers[i];
        }

        if (Speed == 0)
        {
            _usingMoveController = _walkController;
            Speed = _walkController.GetMove();
        }
    }
}