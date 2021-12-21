using Zenject;

public class SlowWalkRunSound : MoveSound
{
    [Inject]
    private void Construct(SlowWalkRunController slowWalkRunController)
    {
        _moveController = slowWalkRunController;
    }
}