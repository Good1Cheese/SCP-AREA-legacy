using Zenject;

public class BobbingWhileSlowWalk : BobbingChangeWhileMoveAction
{
    [Inject]
    private void Construct(SlowWalkController slowWalkController)
    {
        _moveController = slowWalkController;
    }
}