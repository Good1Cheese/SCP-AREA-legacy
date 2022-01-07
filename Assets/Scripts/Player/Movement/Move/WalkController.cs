using UnityEngine;

public class WalkController : MoveController
{
    public override float GetMove()
    {
        _currentStepTime += Time.deltaTime;
        
        if (_currentStepTime >= _targetStepTime)
        {
            Stepped?.Invoke();

            _currentStepTime = 0;

            if (_leftIsLastStep)
            {
                OnLeftStep?.Invoke();
            }
            else
            {
                OnRightStep?.Invoke();
            }
            _leftIsLastStep = !_leftIsLastStep;
        }

        IsMoving = true;
        return Move();
    }

    public override void StopMove()
    {
        IsMoving = false;
        UseStopped?.Invoke();
    }
}