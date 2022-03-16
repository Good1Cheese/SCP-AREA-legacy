using UnityEngine;

public class MoveSteps
{
    private bool _leftIsLast;
    private float _moveTime;

    public void Step(Move move)
    {
        _moveTime += Time.deltaTime;

        if (_moveTime < move.TargetStepTime) { return; }

        _moveTime = 0;
        move.Actions.Stepped?.Invoke();

        DefineLeftOrRightStepMade(move);
    }

    private void DefineLeftOrRightStepMade(Move move)
    {
        if (_leftIsLast)
        {
            move.Actions.OnLeftStep?.Invoke();
            _leftIsLast = false;
            return;
        }

        _leftIsLast = true;
        move.Actions.OnRightStep?.Invoke();
    }
}