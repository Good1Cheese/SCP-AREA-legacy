using UnityEngine;
using Zenject;

public class MovesContainer : MonoBehaviour
{
    [SerializeField] private AnimationCurve _speedCurve;
    [SerializeField] private Move[] _moves;

    private Move _usingMove;
    private MoveSteps _moveSteps;

    public float SlowDownFactor { get; set; }
    public float MoveTime { get; set; }
    public float Speed { get; set; }
    public bool UsingFound { get; set; }

    [Inject]
    private void Construct(MoveSteps moveSteps)
    {
        _moveSteps = moveSteps;
    }

    public float CheckAndReturnSpeed()
    {
        GetCurrentMove();

        _moveSteps.Step(_usingMove);
        _usingMove.UpdateFov();

        Speed = _speedCurve.Evaluate(MoveTime);

        return Speed - SlowDownFactor;
    }

    private void GetCurrentMove()
    {
        UsingFound = false;

        for (int i = 0; i < _moves.Length; i++)
        {
             _moves[i].Use();

            if (!_moves[i].Using) { continue; }

            UsingFound = true;
            _usingMove = _moves[i];
            return;
        }
    }
}