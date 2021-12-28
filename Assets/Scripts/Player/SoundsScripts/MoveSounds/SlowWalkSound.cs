using Zenject;

public class SlowWalkSound : MoveSound
{
    [Inject]
    private void Construct(SlowWalkController slowWalkController)
    {
        _moveController = slowWalkController;
    }
}