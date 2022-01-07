using Zenject;

public class BobbingWhileSlowWalkRun : BobbingChangeWhileMoveAction
{
    [Inject]
    private void Construct(SlowWalkRunController slowWalkRunController)
    {
        _moveController = slowWalkRunController;
    }
}